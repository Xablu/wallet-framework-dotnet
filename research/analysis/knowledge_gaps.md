# Knowledge Gaps

This document outlines the areas where the current research has insufficient information and requires further investigation. These gaps will inform subsequent targeted research cycles.

## Edge-Case Functional Tests - Identified Gaps

-   **Oversized Payloads:** The initial research did not yield specific guidance or best practices for handling excessively large JSON payloads or URIs in .NET applications, particularly within the context of the wallet framework's performance and security requirements. Further research is needed to understand potential vulnerabilities or performance degradation associated with oversized inputs and how to effectively test for these scenarios.
-   **Invalid Credential Configurations:** While the concept of testing invalid configurations is mentioned in the blueprint, the initial research did not provide concrete examples or a comprehensive list of what constitutes an "invalid credential configuration" within the specific domain of the wallet framework (OID4VC, mDoc, SD-JWT). Targeted research is required to define these invalid states precisely to inform the creation of relevant test cases.

## Concurrency & Thread-Safety - Identified Gaps

-   **Parallel Wallet Operations Testing:** The research provided general information on .NET concurrency features and pitfalls, but lacked specific strategies, patterns, or examples for effectively testing parallel wallet record operations against an in-memory store. Further research is needed to determine appropriate testing methodologies and tools for this specific scenario.
-   **Race Condition Testing in PaymentTransactionDataSamples:** The blueprint specifically mentions testing race conditions on `PaymentTransactionDataSamples`. The initial research provided general information on race conditions and synchronization, but did not offer concrete examples or approaches for identifying and testing race conditions within this specific component or similar data structures used in the wallet framework. Targeted research is required to develop effective test cases for these scenarios.

## Negative & Security-Focused Tests - Identified Gaps

-   **Tampered Tokens and Replayed Requests Testing:** The research provided general information on security tokens but lacked specific guidance and techniques for testing against tampered JSON Web Tokens (JWTs) and replayed HTTP requests within the context of the wallet framework's communication protocols (OID4VC, etc.). Further research is needed to understand common attack vectors and effective testing strategies for these scenarios.
-   **Comprehensive CSRF/XSS Testing:** While basic input sanitization was mentioned, the research did not provide comprehensive strategies, tools, or .NET-specific guidance for conducting thorough CSRF (Cross-Site Request Forgery) and XSS (Cross-Site Scripting) checks, particularly relevant for any cookie-based authentication flows the wallet framework might utilize.
-   **FIPS Compliance for Cryptography:** The research highlighted the importance of using cryptographically secure random number generators, but lacked detailed information on the specific steps, configurations, or verification processes required to ensure the wallet framework's cryptographic operations are fully compliant with FIPS standards.
-   **SD-JWT Selective Disclosure Edge Cases:** The research provided no information regarding SD-JWT selective disclosure edge cases, especially concerning the implications and testing of maximum nested claims. This is a significant knowledge gap requiring dedicated research into the SD-JWT specification and related testing methodologies.

## Performance Benchmarks - Identified Gaps

-   **Bulk Serialization/Deserialization Benchmarking:** The research provided general information on .NET serialization performance and optimization techniques, but lacked specific strategies, tools, or examples for benchmarking the performance of bulk serialization and deserialization of a large number of records (e.g., 1000), which is a key performance benchmark identified in the blueprint.
-   **High-Throughput Issuance Simulation:** The research did not provide information or strategies for designing and implementing a simulation of high-throughput credential issuance for performance testing within the wallet framework. This is a knowledge gap that needs to be addressed to effectively benchmark this critical operation.

## Compliance Scenarios - Identified Gaps

-   **OID4VC, mDoc, and SD-JWT Cryptographic Compliance:** The research provided general information on .NET cryptography features but lacked specific details on the cryptographic algorithms, key sizes, and protocol requirements mandated by the OID4VC, mDoc, and SD-JWT specifications. Further research is needed to ensure the wallet framework's cryptographic implementations align with these standards.
-   **FIPS Compliance Verification for Wallet Framework:** While .NET's FIPS mode configuration was mentioned, the research did not provide concrete steps, tools, or verification methods specifically for ensuring and demonstrating FIPS compliance of the wallet framework's cryptographic modules and operations.
-   **SD-JWT Selective Disclosure Compliance Aspects:** The research provided no information on the specific compliance requirements or testing methodologies related to SD-JWT selective disclosure, including how to ensure compliance when handling and verifying selectively disclosed claims, especially in complex scenarios with nested claims. This is a significant knowledge gap requiring dedicated research into the SD-JWT specification and compliance testing.