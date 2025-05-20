# Feature: Handling of Large and Complex Credential Data

## Scenario: Wallet can handle credentials with large or complex data

Given a user has a wallet
And an issuer is available that can issue credentials with a large number of claims or complex nested data structures
When the user receives and accepts an offer for a credential with large/complex data (simulated user action)
Then the credential should be securely stored in the wallet without data loss or corruption
When a verifier requests a presentation of claims from the large/complex credential
Then the wallet should successfully present the requested claims without performance issues

**AI Verifiable Completion Criterion:** The wallet successfully ingests, stores, and presents credentials containing a large volume of data or deeply nested claims without performance degradation or data corruption, verifiable by issuing and presenting test credentials with complex data structures and confirming data integrity and performance metrics via API interactions.