# Feature: Selective Disclosure with SD-JWT

## Scenario: Wallet correctly performs selective disclosure for SD-JWT credentials

Given a user has a wallet containing an SD-JWT credential with multiple claims
And a verifier requests a presentation of a specific subset of claims from the SD-JWT credential
When the user receives the presentation request and approves the disclosure of the requested claims (simulated user action)
Then the wallet should generate a presentation containing only the approved claims
And the verifier should successfully verify the presentation with the selectively disclosed claims

**AI Verifiable Completion Criterion:** When presenting an SD-JWT credential, the wallet only discloses the claims explicitly requested by the verifier and selected by the user (simulated), verifiable by examining the presented credential data sent to the verifier's endpoint and confirming that only the intended claims are included.