# High-Level Architecture: Wallet Framework .NET

## 1. Introduction

This document defines the high-level architecture for the Wallet Framework .NET project. It outlines the major components, their responsibilities, interactions, and the overall structure of the system. This architecture is designed to support the project's goal of providing a robust and testable .NET-based digital wallet framework, directly aligning with the AI verifiable tasks defined in [`docs/PRDMasterPlan.md`](docs/PRDMasterPlan.md) and enabling the successful execution of the high-level acceptance tests detailed in [`docs/master_acceptance_test_plan.md`](docs/master_acceptance_test_plan.md). As a foundational architectural step, this design serves as the blueprint for subsequent development and scaffolding activities.

## 2. Architectural Style

The architecture follows a modular design, separating concerns into distinct components that interact through well-defined interfaces. This promotes maintainability, testability, and flexibility, allowing for potential future extensions or alternative implementations of specific components (e.g., different storage mechanisms or identity layer integrations).

## 3. High-Level Components

The Wallet Framework .NET is composed of the following key high-level components:

*   **Wallet Core:** The central component responsible for managing the overall wallet state, user identity (in coordination with the Identity Layer), and providing core wallet functionalities. It orchestrates interactions between other components.
*   **Credential Management:** An abstraction layer that provides a unified interface for handling different types of digital credentials (mdoc, SD-JWT, etc.). It delegates format-specific operations to dedicated modules.
*   **mdoc Module:** Responsible for the specific logic related to mdoc credentials, including parsing, validation, storage formatting, and presentation formatting.
*   **SD-JWT Module:** Responsible for the specific logic related to SD-JWT credentials, including parsing, validation, storage formatting, presentation formatting, and handling selective disclosure.
*   **OIDC4VCI Module:** Implements the OIDC for Verifiable Credential Issuance protocol flow. It handles receiving credential offers, interacting with the user (simulated at this level), requesting credentials from issuers, and passing received credentials to the Credential Management component for storage.
*   **OIDC4VP Module:** Implements the OIDC for Verifiable Presentation protocol flow. It handles receiving presentation requests, interacting with the user (simulated), retrieving credentials via the Credential Management component, generating presentations (including selective disclosure for SD-JWT), and sending presentations to verifiers.
*   **Decentralized Identity Layer Integration:** An adapter or service that interfaces with an underlying decentralized identity framework (such as Hyperledger Aries .NET, as suggested by the existing codebase structure). This component handles DID management, key management, secure messaging, and potentially interactions with ledgers.
*   **Secure Storage Service:** Provides a secure mechanism for storing sensitive wallet data, including encrypted credentials and private keys (managed in coordination with the Identity Layer). It offers interfaces for saving, retrieving, and deleting data securely.
*   **API/Interface Layer:** Exposes the functionality of the Wallet Framework to external applications, such as a mobile wallet application or a backend service. This layer will define the public API contracts for interacting with the wallet.

## 4. Key Interactions and Data Flows

### 4.1. Credential Issuance Flow (OIDC4VCI)

1.  An external entity (e.g., a mobile app) receives a credential offer URI and invokes the **API/Interface Layer**.
2.  The **API/Interface Layer** forwards the request to the **OIDC4VCI Module**.
3.  The **OIDC4VCI Module** fetches the credential offer details from the Issuer.
4.  The **OIDC4VCI Module** interacts with the **Wallet Core** to potentially involve user consent (simulated).
5.  The **OIDC4VCI Module** requests the credential from the Issuer, potentially using secure messaging capabilities provided by the **Decentralized Identity Layer Integration**.
6.  The Issuer issues the credential (in mdoc or SD-JWT format).
7.  The **OIDC4VCI Module** receives the credential and passes it to the **Credential Management** component.
8.  The **Credential Management** component identifies the credential format and delegates parsing and validation to the appropriate **mdoc Module** or **SD-JWT Module**.
9.  The format-specific module processes the credential and prepares it for storage.
10. The format-specific module interacts with the **Secure Storage Service** to encrypt and store the credential data.
11. The **Wallet Core** is updated with the new credential information.
12. A response is returned through the **API/Interface Layer**.

### 4.2. Credential Presentation Flow (OIDC4VP)

1.  An external entity receives a presentation request (e.g., OIDC4VP URI) and invokes the **API/Interface Layer**.
2.  The **API/Interface Layer** forwards the request to the **OIDC4VP Module**.
3.  The **OIDC4VP Module** parses the presentation request, potentially fetching details from the Verifier.
4.  The **OIDC4VP Module** interacts with the **Wallet Core** and **Credential Management** component to identify potential credentials that match the request's requirements.
5.  The **Credential Management** component retrieves relevant credentials from the **Secure Storage Service** (which decrypts them).
6.  The **OIDC4VP Module** interacts with the user (simulated) via the **Wallet Core** to select credentials and claims (including selective disclosure for SD-JWT, handled by the **SD-JWT Module**).
7.  The appropriate format-specific module (**mdoc Module** or **SD-JWT Module**) generates the verifiable presentation based on the selected data.
8.  The **OIDC4VP Module** sends the verifiable presentation to the Verifier, potentially using secure messaging capabilities provided by the **Decentralized Identity Layer Integration**.
9.  A response is returned through the **API/Interface Layer**.

## 5. Technology Stack

*   **Core Development Language:** C#
*   **Framework:** .NET
*   **Decentralized Identity:** Hyperledger Aries .NET (integration layer)
*   **Credential Formats:** Libraries for mdoc and SD-JWT processing (to be implemented or integrated).
*   **Storage:** Abstract storage interface with potential implementations for different platforms (e.g., secure enclave, encrypted file system, database).
*   **Testing:** xUnit, SpecFlow, FsCheck, BenchmarkDotNet.
*   **CI/CD:** GitHub Actions.

## 6. Alignment with PRDMasterPlan.md and High-Level Acceptance Tests

This high-level architecture directly supports the AI verifiable tasks outlined in [`docs/PRDMasterPlan.md`](docs/PRDMasterPlan.md) and is designed to enable the successful execution of the high-level acceptance tests in [`docs/master_acceptance_test_plan.md`](docs/master_acceptance_test_plan.md).

*   **Credential Issuance Flow (OIDC for VCI):** Handled by the **OIDC4VCI Module**, interacting with **Wallet Core**, **Credential Management**, and **Secure Storage Service**.
*   **Credential Presentation Flow (OIDC for VP):** Handled by the **OIDC4VP Module**, interacting with **Wallet Core**, **Credential Management**, and **Secure Storage Service**.
*   **Handling of Different Credential Formats (mdoc and SD-JWT):** Supported by dedicated **mdoc Module** and **SD-JWT Module** components, orchestrated by **Credential Management**.
*   **Secure Storage and Retrieval of Credentials:** Provided by the **Secure Storage Service**.
*   **Interaction with Decentralized Identity Layer:** Managed by the **Decentralized Identity Layer Integration** component.
*   **Error Handling During Flows:** Needs to be implemented within each module, with errors propagated through the **API/Interface Layer**.
*   **Selective Disclosure with SD-JWT:** Specifically handled by the **SD-JWT Module** during the presentation flow.
*   **Handling of Large and Complex Credential Data:** Needs to be considered in the design of the **mdoc Module**, **SD-JWT Module**, and **Secure Storage Service**.

The modular nature of the architecture facilitates the implementation of unit, integration, and E2E tests as required by the SPARC Acceptance phase tasks in the PRD. The defined components provide clear boundaries for writing focused tests.

## 7. Considerations

*   **Security:** Secure handling of private keys and sensitive data is paramount. The **Secure Storage Service** and **Decentralized Identity Layer Integration** are critical components for this. All interactions involving sensitive data must be carefully designed and reviewed.
*   **Performance:** The architecture should consider performance implications, especially when handling large numbers of credentials or complex data structures. Efficient algorithms and data structures should be used within the format-specific modules and storage service.
*   **Scalability:** While this is a client-side wallet framework, the architecture should not preclude its use in scenarios requiring handling a moderate number of credentials.
*   **Maintainability:** The modular design with clear interfaces promotes maintainability. Code within each module should adhere to .NET best practices and coding standards.
*   **Extensibility:** The architecture should allow for the addition of new credential formats or protocol versions in the future with minimal impact on existing components.

## 8. Future Work and Refinements

This high-level architecture provides the initial structure. Future work will involve:

*   Detailed design of each component, including specific classes, interfaces, and data models.
*   Selection of specific libraries for mdoc and SD-JWT processing, or detailed design for their implementation.
*   Detailed design of the **Secure Storage Service** interface and potential platform-specific implementations.
*   Definition of the API contracts for the **API/Interface Layer**.
*   Implementation of the scaffolding based on this architecture.

This architecture document will serve as a living document, updated as the design evolves and more detailed decisions are made.