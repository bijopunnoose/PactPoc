# Pact standalone executables

This package contains the Ruby implementations of the Pact Mock Service, Pact Provider Verifier and Pact Broker Client, packaged with Travelling Ruby so that they can be run from the command line without a Ruby installation.

## Package contents

This version (1.22.1) of the Pact standalone executables package contains:

  * pact gem 1.20.0
  * pact-mock_service gem 2.6.3
  * pact-support gem 1.2.4
  * pact-provider-verifier gem 1.11.0
  * pact_broker-client gem 1.13.1

## Usage

<a name="pact-mock-service"></a>
### pact-mock-service

```
Commands:
  pact-mock-service control               # Run a Pact mock service control s...
  pact-mock-service control-restart       # Start a Pact mock service control...
  pact-mock-service control-start         # Start a Pact mock service control...
  pact-mock-service control-stop          # Stop a Pact mock service control ...
  pact-mock-service help [COMMAND]        # Describe available commands or on...
  pact-mock-service restart               # Start or restart a mock service. ...
  pact-mock-service service               # Start a mock service. If the cons...
  pact-mock-service start                 # Start a mock service. If the cons...
  pact-mock-service stop -p, --port=PORT  # Stop a Pact mock service
  pact-mock-service version               # Show the pact-mock-service gem ve...

Usage:
  pact-mock-service service

Options:
      [--consumer=CONSUMER]                                      # Consumer name
      [--provider=PROVIDER]                                      # Provider name
  -p, [--port=PORT]                                              # Port on which to run the service
  -h, [--host=HOST]                                              # Host on which to bind the service
                                                                 # Default: localhost
  -d, [--pact-dir=PACT_DIR]                                      # Directory to which the pacts will be written
  -m, [--pact-file-write-mode=PACT_FILE_WRITE_MODE]              # `overwrite` or `merge`. Use `merge` when running multiple mock service instances in parallel for the same consumer/provider pair. Ensure the pact file is deleted before running tests when using this option so that interactions deleted from the code are not maintained in the file.
                                                                 # Default: overwrite
  -i, [--pact-specification-version=PACT_SPECIFICATION_VERSION]  # The pact specification version to use when writing the pact. Note that only versions 1 and 2 are currently supported.
                                                                 # Default: 2
  -l, [--log=LOG]                                                # File to which to log output
  -o, [--cors=CORS]                                              # Support browser security in tests by responding to OPTIONS requests and adding CORS headers to mocked responses
      [--ssl], [--no-ssl]                                        # Use a self-signed SSL cert to run the service over HTTPS
      [--sslcert=SSLCERT]                                        # Specify the path to the SSL cert to use when running the service over HTTPS
      [--sslkey=SSLKEY]                                          # Specify the path to the SSL key to use when running the service over HTTPS

Start a mock service. If the consumer, provider and pact-dir options are provided, the pact will be written automatically on shutdown (INT).

```

<a name="pact-stub-service"></a>
### pact-stub-service

```
Usage:
  pact-stub-service PACT ...

Options:
  -p, [--port=PORT]        # Port on which to run the service
  -h, [--host=HOST]        # Host on which to bind the service
                           # Default: localhost
  -l, [--log=LOG]          # File to which to log output
  -o, [--cors=CORS]        # Support browser security in tests by responding to OPTIONS requests and adding CORS headers to mocked responses
      [--ssl], [--no-ssl]  # Use a self-signed SSL cert to run the service over HTTPS
      [--sslcert=SSLCERT]  # Specify the path to the SSL cert to use when running the service over HTTPS
      [--sslkey=SSLKEY]    # Specify the path to the SSL key to use when running the service over HTTPS

Description:
  Start a stub service with the given pact file(s). Where multiple matching 
  interactions are found, the interactions will be sorted by response status, 
  and the first one will be returned. This may lead to some non-deterministic 
  behaviour. If you are having problems with this, please raise it on the 
  pact-dev google group, and we can discuss some potential enhancements. Note 
  that only versions 1 and 2 of the pact specification are currently supported.

```

<a name="pact-provider-verifier"></a>
### pact-provider-verifier

```
Usage:
  pact-provider-verifier PACT_URL ... -h, --provider-base-url=PROVIDER_BASE_URL

Options:
  -h, --provider-base-url=PROVIDER_BASE_URL                          # Provider host URL
  -c, [--provider-states-setup-url=PROVIDER_STATES_SETUP_URL]        # Base URL to setup the provider states at
  -a, [--provider-app-version=PROVIDER_APP_VERSION]                  # Provider application version, required when publishing verification results
  -r, [--publish-verification-results=PUBLISH_VERIFICATION_RESULTS]  # Publish verification results to the broker
  -n, [--broker-username=BROKER_USERNAME]                            # Pact Broker basic auth username
  -p, [--broker-password=BROKER_PASSWORD]                            # Pact Broker basic auth password
      [--custom-provider-header=CUSTOM_PROVIDER_HEADER]              # Header to add to provider state set up and pact verification requests. eg 'Authorization: Basic cGFjdDpwYWN0'. May be specified multiple times.
  -v, [--verbose=VERBOSE]                                            # Verbose output
  -f, [--format=FORMATTER]                                           # RSpec formatter. Defaults to custom Pact formatter. json and RspecJunitFormatter may also be used.
  -u, [--pact-urls=PACT_URLS]                                        # DEPRECATED. Please provide as space separated arguments.

Verify pact(s) against a provider. Supports local and networked (http-based) files.

```

<a name="pact-broker"></a>
### pact-broker client

```
Usage:
  pact-broker publish PACT_DIRS_OR_FILES ... -a, --consumer-app-version=CONSUMER_APP_VERSION -b, --broker-base-url=BROKER_BASE_URL

Options:
  -a, --consumer-app-version=CONSUMER_APP_VERSION          # The consumer application version
  -b, --broker-base-url=BROKER_BASE_URL                    # The base URL of the Pact Broker
  -u, [--broker-username=BROKER_USERNAME]                  # Pact Broker basic auth username
  -p, [--broker-password=BROKER_PASSWORD]                  # Pact Broker basic auth password
  -t, [--tag=TAG]                                          # Tag name for consumer version. Can be specified multiple times.
  -g, [--tag-with-git-branch], [--no-tag-with-git-branch]  # Tag consumer version with the name of the current git branch. Default: false
  -v, [--verbose], [--no-verbose]                          # Verbose output. Default: false

Publish pacts to a Pact Broker.

```
