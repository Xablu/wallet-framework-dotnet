Feature: Wallet Operations
  As a wallet user
  I want to perform basic wallet operations
  So that I can manage my credentials

Scenario: Successfully issue a credential
  Given a running issuer and wallet
  When the wallet requests a credential from the issuer
  Then the wallet should receive the credential
  And the credential should be stored in the wallet

Scenario: Successfully present a credential
  Given a wallet with a stored credential
  And a verifier requesting a presentation
  When the wallet presents the credential to the verifier
  Then the verifier should successfully verify the credential