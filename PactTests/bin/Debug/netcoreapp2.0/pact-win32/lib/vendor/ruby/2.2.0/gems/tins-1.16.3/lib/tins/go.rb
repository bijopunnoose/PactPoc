module Tins
  module GO
    module EnumerableExtension
      def push(argument)
        @arguments ||= []
        @arguments.push argument
        self
      end
      alias << push

      def each(&block)
        @arguments.each(&block)
        self
      end
      include Enumerable
    end

    module_function

    # Parses the argument array _args_, according to the pattern _s_, to
    # retrieve the single character command line options from it. If _s_ is
    # 'xy:' an option '-x' without an option argument is searched, and an
    # option '-y foo' with an option argument ('foo').
    #
    # An option hash is returned with all found options set to true or the
    # found option argument.
    def go(s, args = ARGV)
      b, v = s.scan(/(.)(:?)/).inject([ {}, {} ]) { |t, (o, a)|
        a = a == ':'
        t[a ? 1 : 0][o] = a ? nil : false
        t
      }
      r = []
      while a = args.shift
        /\A-(?<p>.+)/ =~ a or (r << a; next)
        until p == ''
          o = p.slice!(0, 1)
          if v.key?(o)
            if p.empty? && args.empty?
              r << a
              break 1
            elsif p == ''
              a = args.shift
            else
              a = p
            end
            if v[o].nil?
              a = a.dup
              a.extend EnumerableExtension
              a << a
              v[o] = a
            else
              v[o] << a
            end
            break
          elsif b.key?(o)
            if b[o]
              b[o] += 1
            else
              b[o] = 1
            end
          else
            r << a
          end
        end && break
      end
      args.replace r
      b.merge(v)
    end
  end
end

require 'tins/alias'
