Feature: Wallet Operations

  As a wallet user
  I want to be able to perform basic wallet operations
  So that I can manage my digital credentials

Scenario: Create a new wallet
  Given the wallet service is available
  When I create a new wallet
  Then a new wallet should be created successfully