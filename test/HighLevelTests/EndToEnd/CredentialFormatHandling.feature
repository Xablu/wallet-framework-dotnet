# Feature: Handling of Different Credential Formats (mdoc and SD-JWT)

## Scenario: Wallet can receive, store, and present mdoc and SD-JWT credentials

Given a user has a wallet
And an issuer is available that can issue credentials in mdoc format
And another issuer is available that can issue credentials in SD-JWT format
When the user receives and accepts an mdoc credential offer (simulated user action)
And the user receives and accepts an SD-JWT credential offer (simulated user action)
Then both the mdoc and SD-JWT credentials should be securely stored in the wallet
When a verifier requests a presentation of claims from the mdoc credential
Then the wallet should successfully present the requested claims from the mdoc credential
When a verifier requests a presentation of claims from the SD-JWT credential
Then the wallet should successfully present the requested claims from the SD-JWT credential

**AI Verifiable Completion Criterion:** The wallet successfully ingests and stores credentials provided in both mdoc and SD-JWT formats, and can successfully present claims from both formats upon request, verifiable by issuing and presenting test credentials of each format and confirming the correct data is stored and presented via API interactions.