# For assistance debugging failures

* The pact files have been stored locally in the following temp directory:
    C:/Testing/PACT/FixtureServicePactTest/PactTests/bin/Debug/netcoreapp2.0/tmp/pacts

* The requests and responses are logged in the following log file:
    C:/Testing/PACT/FixtureServicePactTest/PactTests/bin/Debug/netcoreapp2.0/log/pact.log

* Add BACKTRACE=true to the `rake pact:verify` command to see the full backtrace

* If the diff output is confusing, try using another diff formatter.
  The options are :unix, :embedded and :list

    Pact.configure do | config |
      config.diff_formatter = :embedded
    end

  See https://github.com/realestate-com-au/pact/blob/master/documentation/configuration.md#diff_formatter for examples and more information.

* Check out https://github.com/realestate-com-au/pact/wiki/Troubleshooting

* Ask a question on stackoverflow and tag it `pact-ruby`


The following changes have been made since the previous distinct version of this pact, and may be responsible for verification failure:

No previous distinct version was found for Pact between EventAPIConsumer (v1.1.1.1) and OperationServices