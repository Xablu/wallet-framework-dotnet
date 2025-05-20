# Feature: Credential Presentation Flow (OIDC for VP)

## Scenario: Successful presentation of a credential with selective disclosure

Given a user has a wallet containing a stored credential
And a verifier is available and requests a presentation via OIDC for VP
When the user receives the presentation request and selects claims for disclosure (simulated user action)
Then a valid presentation should be generated and sent to the verifier
And the verifier should successfully verify the presentation

**AI Verifiable Completion Criterion:** The wallet successfully receives a presentation request, the user selects the appropriate credential and claims (simulated), a valid presentation is generated and sent to the verifier, and the verifier successfully verifies the presentation, verifiable by monitoring the verifier's API response for a success status and confirmation of the presented data's validity.