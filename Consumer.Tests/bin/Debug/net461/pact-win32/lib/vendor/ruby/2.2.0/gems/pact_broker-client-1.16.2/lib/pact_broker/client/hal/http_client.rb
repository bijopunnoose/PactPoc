require 'pact_broker/client/retry'

module PactBroker
  module Client
    module Hal
      class HttpClient
        attr_reader :username, :password, :verbose

        def initialize options
          @username = options[:username]
          @password = options[:password]
          @verbose = options[:verbose]
        end

        def get href, params = {}, headers = {}
          query = params.collect{ |(key, value)| "#{CGI::escape(key)}=#{CGI::escape(value)}" }.join("&")
          uri = URI(href)
          uri.query = query
          perform_request(create_request(uri, 'Get', nil, headers), uri)
        end

        def put href, body = nil, headers = {}
          uri = URI(href)
          perform_request(create_request(uri, 'Put', body, headers), uri)
        end

        def post href, body = nil, headers = {}
          uri = URI(href)
          perform_request(create_request(uri, 'Post', body, headers), uri)
        end

        def create_request uri, http_method, body = nil, headers = {}
          request = Net::HTTP.const_get(http_method).new(uri.request_uri)
          request['Content-Type'] = "application/json" if ['Post', 'Put', 'Patch'].include?(http_method)
          request['Accept'] = "application/hal+json"
          headers.each do | key, value |
            request[key] = value
          end

          request.body = body if body
          request.basic_auth username, password if username
          request
        end

        def perform_request request, uri
          response = Retry.while_error do
            http = Net::HTTP.new(uri.host, uri.port, :ENV)
            http.set_debug_output($stderr) if verbose
            http.use_ssl = (uri.scheme == 'https')
            http.start do |http|
              http.request request
            end
          end
          Response.new(response)
        end

        class Response < SimpleDelegator
          def body
            bod = __getobj__().body
            if bod && bod != ''
              JSON.parse(bod)
            else
              nil
            end
          end

          def raw_body
            __getobj__().body
          end

          def status
            code.to_i
          end

          def success?
            __getobj__().code.start_with?("2")
          end
        end
      end
    end
  end
end
