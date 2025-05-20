# Feature: Credential Issuance Flow (OIDC for VCI)

## Scenario: Successful issuance of a credential

Given a user has a wallet
And an issuer is available and offers a credential via OIDC for VCI
When the user receives and accepts the credential offer (simulated user action)
Then the credential should be securely stored in the wallet

**AI Verifiable Completion Criterion:** The wallet successfully receives a credential offer, the user accepts it (simulated), and the credential is securely stored in the wallet, verifiable by querying the wallet's contents via a defined API endpoint and confirming the presence and integrity of the newly issued credential.