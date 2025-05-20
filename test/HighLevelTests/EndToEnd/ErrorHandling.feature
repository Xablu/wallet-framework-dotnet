# Feature: Error Handling During Flows

## Scenario: Wallet gracefully handles errors during issuance and presentation

Given a user is performing a credential issuance or presentation flow
When an invalid offer/request is received or a network error occurs (simulated)
Then the wallet should display an appropriate error message to the user (simulated/checked via UI or API)
And the wallet should remain in a stable state without crashing

**AI Verifiable Completion Criterion:** When presented with invalid input or simulated network errors during issuance or presentation flows, the wallet displays appropriate error messages to the user (simulated/checked via UI or API response) and maintains a stable state without crashing, verifiable by injecting errors or invalid data and confirming the expected error handling behavior via API responses or simulated UI checks.