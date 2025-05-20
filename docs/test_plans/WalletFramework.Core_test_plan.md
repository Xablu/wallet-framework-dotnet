# Granular Test Plan: WalletFramework.Core

## 1. Introduction

This document outlines the granular test plan for the `WalletFramework.Core` module. It details the testing scope, strategy, individual test cases, and recursive testing approach, adhering to London School of TDD principles. The tests defined herein are designed to verify the correct behavior of the core utilities and foundational components within this module, which are critical building blocks for the higher-level functionalities described in the Master Project Plan and Master Acceptance Test Plan.

## 2. Test Scope

The scope of this test plan is limited to the public interfaces and observable behavior of the components within the `WalletFramework.Core` module. The tests will focus on verifying that these components function correctly in isolation and interact as expected with their immediate collaborators.

These granular tests directly contribute to achieving the following AI Verifiable End Results from [`docs/PRDMasterPlan.md`](docs/PRDMasterPlan.md):

*   **Phase 3: Acceptance, Micro Task 1:** "Test files created in the respective test projects (e.g., `WalletFramework.Core.Tests/UtilsTests.cs`), and test runner output shows all implemented unit tests passing." - The test cases defined here provide the blueprint for these test files and their expected passing state.

While not directly verifying the high-level acceptance tests in [`docs/master_acceptance_test_plan.md`](docs/master_acceptance_test_plan.md), the correct functioning of `WalletFramework.Core` components is essential for the successful execution and verification of those end-to-end flows (e.g., correct Base64Url encoding is needed for OID4VC messages, correct ClaimPath parsing is needed for presentation requests).

## 3. Test Strategy: London School TDD and Recursive Testing

### 3.1. London School TDD Principles

Testing for `WalletFramework.Core` will strictly follow the London School of TDD (also known as Mockist TDD). This approach emphasizes testing the behavior of a unit by observing its interactions with its collaborators, rather than inspecting its internal state.

*   **Focus on Behavior:** Tests will verify that a method or class sends the correct messages to its collaborators and produces the expected observable output or side effect.
*   **Mocking Collaborators:** Dependencies and collaborators will be replaced with test doubles (mocks or stubs) to isolate the unit under test. This allows verification of the interactions between the unit and its dependencies without relying on the actual implementation of those dependencies.
*   **Outcome Verification:** Test success will be determined by verifying the observable outcome of the unit's execution, such as return values, exceptions thrown, or the sequence and arguments of calls made to mocked collaborators.

### 3.2. Recursive Testing Strategy (Frequent Regression)

A comprehensive recursive testing strategy will be employed to ensure the ongoing stability of the `WalletFramework.Core` module and catch regressions early.

*   **Triggers for Re-execution:**
    *   **Every Commit/Pull Request:** A subset of critical, fast-running tests (smoke tests, core utility tests) will run on every commit or pull request to provide rapid feedback.
    *   **Code Changes in `WalletFramework.Core`:** All tests within `WalletFramework.Core.Tests` will run when code in the `src/WalletFramework.Core` directory changes.
    *   **Code Changes in Dependent Modules:** Relevant `WalletFramework.Core.Tests` (specifically those verifying interactions used by the dependent module) will be included in regression runs when modules that depend on `WalletFramework.Core` (e.g., `WalletFramework.Oid4Vc`, `WalletFramework.MdocLib`) are modified.
    *   **Scheduled Builds (e.g., Nightly):** A full regression suite, including all `WalletFramework.Core.Tests`, will run on a scheduled basis.
    *   **Before Merging to `main`:** A full regression suite will run to ensure stability before integrating changes into the main development branch.
*   **Test Prioritization and Tagging:** Tests will be tagged using test framework attributes (e.g., `[Trait("Category", "Base64Url")]`, `[Trait("Impact", "Critical")]`) to facilitate selection for different regression scopes. Critical utility tests will be prioritized for faster feedback loops.
*   **Test Selection for Regression:**
    *   **Smoke/Critical Subset:** Tests tagged as "Critical" or belonging to core, frequently used utilities (Base64Url, Functional helpers) will be selected for per-commit/PR runs.
    *   **Module-Specific Subset:** All tests within `WalletFramework.Core.Tests` will be selected when `WalletFramework.Core` code changes.
    *   **Dependency-Aware Subset:** CI configuration will identify modules dependent on `WalletFramework.Core` and include relevant `Core` tests in their regression runs.
    *   **Full Suite:** All `WalletFramework.Core.Tests` will be selected for scheduled and pre-merge runs.

## 4. Granular Test Cases

This section details specific test cases for key functionalities within `WalletFramework.Core`. Each test case maps to relevant AI Verifiable End Results from the Master Project Plan and includes an AI verifiable completion criterion.

### 4.1. Base64Url Encoding and Decoding

*   **Unit Under Test:** `WalletFramework.Core.Base64Url.Base64UrlEncoder` and `WalletFramework.Core.Base64Url.Base64UrlDecoder`.
*   **Relevant AI Verifiable End Result (from PRDMasterPlan.md):** Phase 3, Micro Task 1 (Unit Tests Passing).
*   **Interactions to Test:**
    *   Encoding byte arrays to Base64Url strings.
    *   Decoding Base64Url strings back to byte arrays.
    *   Handling edge cases (empty input, specific characters).
*   **Collaborators to Mock/Stub:** None (these are pure utility functions).
*   **Observable Outcome Verification:**
    *   Encoding a known byte array results in the expected Base64Url string.
    *   Decoding a known Base64Url string results in the original byte array.
    *   Decoding an invalid Base64Url string throws the expected error (`Base64UrlStringDecodingError`).
*   **Recursive Scope:** Included in Smoke/Critical Subset, Module-Specific Subset, Dependency-Aware Subset (for modules using Base64Url), Full Suite.
*   **AI Verifiable Completion Criterion:** Test runner output confirms that the test method `Base64UrlEncoder_EncodesCorrectly` passes, the test method `Base64UrlDecoder_DecodesCorrectly` passes, and the test method `Base64UrlDecoder_ThrowsErrorForInvalidInput` passes.

### 4.2. ClaimPath Parsing and Selection

*   **Unit Under Test:** `WalletFramework.Core.ClaimPaths.ClaimPath` and related selection logic.
*   **Relevant AI Verifiable End Result (from PRDMasterPlan.md):** Phase 3, Micro Task 1 (Unit Tests Passing).
*   **Interactions to Test:**
    *   Parsing a string representation into a `ClaimPath` object.
    *   Selecting values from a JSON structure using a `ClaimPath`.
    *   Handling different component types (object properties, array indices, wildcards).
    *   Handling invalid claim paths or paths that do not match the JSON structure.
*   **Collaborators to Mock/Stub:** None (operates on data structures).
*   **Observable Outcome Verification:**
    *   Parsing a valid claim path string results in a correctly structured `ClaimPath` object.
    *   Selecting data from a JSON object using a valid claim path returns the expected JSON value(s).
    *   Attempting to parse an invalid claim path string throws the expected error (`ClaimPathError` or specific subclass).
    *   Attempting to select data using a claim path that doesn't match the JSON structure throws the expected error (e.g., `ElementNotFoundError`).
*   **Recursive Scope:** Included in Module-Specific Subset, Dependency-Aware Subset (for modules using ClaimPaths, e.g., OID4VP), Full Suite.
*   **AI Verifiable Completion Criterion:** Test runner output confirms that test methods covering valid parsing, successful selection for various path types, and error handling for invalid paths/selections all pass.

### 4.3. Functional Programming Helpers (Option, Error, Validation)

*   **Unit Under Test:** `WalletFramework.Core.Functional.OptionFun`, `WalletFramework.Core.Functional.Error`, `WalletFramework.Core.Functional.Validation`, and related extensions.
*   **Relevant AI Verifiable End Result (from PRDMasterPlan.md):** Phase 3, Micro Task 1 (Unit Tests Passing).
*   **Interactions to Test:**
    *   Creating `Option` instances (Some, None).
    *   Mapping and binding operations on `Option`.
    *   Creating `Error` instances.
    *   Using `Validation` for accumulating errors.
    *   Combining functional constructs.
*   **Collaborators to Mock/Stub:** None (pure functional constructs).
*   **Observable Outcome Verification:**
    *   Mapping/binding operations on `Option` produce the expected `Option` state (Some or None) and value.
    *   `Validation` correctly accumulates errors or returns a successful result.
    *   Combining operations yield the expected final `Option`, `Error`, or `Validation` state.
*   **Recursive Scope:** Included in Smoke/Critical Subset, Module-Specific Subset, Dependency-Aware Subset (as these are widely used), Full Suite.
*   **AI Verifiable Completion Criterion:** Test runner output confirms that test methods verifying the behavior of `Option`, `Error`, and `Validation` operations pass for various scenarios (success, failure, edge cases).

### 4.4. JSON Utilities

*   **Unit Under Test:** `WalletFramework.Core.Json.JsonFun` and related extensions.
*   **Relevant AI Verifiable End Result (from PRDMasterPlan.md):** Phase 3, Micro Task 1 (Unit Tests Passing).
*   **Interactions to Test:**
    *   Parsing JSON strings.
    *   Extracting values from JSON structures by key or path.
    *   Handling different JSON types (objects, arrays, primitives).
    *   Handling invalid JSON or missing fields.
*   **Collaborators to Mock/Stub:** None (operates on strings/data structures).
*   **Observable Outcome Verification:**
    *   Parsing a valid JSON string results in the expected JToken structure.
    *   Extracting a value using a valid key/path returns the correct JToken or primitive value.
    *   Attempting to parse invalid JSON throws the expected error (`InvalidJsonError`).
    *   Attempting to extract a missing field throws the expected error (`JsonFieldNotFoundError`).
*   **Recursive Scope:** Included in Smoke/Critical Subset, Module-Specific Subset, Dependency-Aware Subset (as JSON is fundamental), Full Suite.
*   **AI Verifiable Completion Criterion:** Test runner output confirms that test methods verifying JSON parsing, value extraction, and error handling for invalid JSON or missing fields all pass.

### 4.5. Cryptography Utilities

*   **Unit Under Test:** `WalletFramework.Core.Cryptography.CryptoUtils` and related models (`PublicKey`, `RawSignature`).
*   **Relevant AI Verifiable End Result (from PRDMasterPlan.md):** Phase 3, Micro Task 1 (Unit Tests Passing).
*   **Interactions to Test:**
    *   Verifying digital signatures using a public key and raw signature bytes.
    *   Handling valid and invalid signatures.
*   **Collaborators to Mock/Stub:** An abstraction for cryptographic operations (`IKeyStore` or similar if used by `CryptoUtils`, otherwise none for static methods). For signature verification, the underlying crypto library's verification function would be the dependency, but we test the `CryptoUtils` wrapper's behavior.
*   **Observable Outcome Verification:**
    *   Verifying a valid signature returns a success indication (e.g., `true` or a successful `Validation` result).
    *   Verifying an invalid signature returns a failure indication (e.g., `false` or an `InvalidSignatureError`).
*   **Recursive Scope:** Included in Smoke/Critical Subset, Module-Specific Subset, Dependency-Aware Subset (for modules performing signature verification, e.g., SD-JWT, mdoc), Full Suite.
*   **AI Verifiable Completion Criterion:** Test runner output confirms that test methods verifying signature validation for valid and invalid signatures pass.

## 5. Test Data and Mock Configurations

*   **Test Data:**
    *   **Base64Url:** Various byte arrays and their expected Base64Url encoded string representations, including empty arrays and data containing characters that require URL-safe encoding. Invalid Base64Url strings.
    *   **ClaimPath:** Valid claim path strings covering object properties, array indices, and wildcards. JSON structures matching these paths. Invalid claim path strings. JSON structures that do not match valid claim paths.
    *   **Functional:** Various inputs to functional operations to test success and failure paths for `Option`, `Error`, and `Validation`.
    *   **JSON:** Valid JSON strings of various structures and complexities. Invalid JSON strings. JSON structures with missing or null fields.
    *   **Cryptography:** Valid public keys, raw signatures, and original data. Invalid signatures.
*   **Mock Configurations:** For `WalletFramework.Core`, direct mocking of collaborators is expected to be minimal as it primarily contains pure utility functions and data structures. If any components are introduced that depend on external services or complex objects, mocks will be configured using a mocking framework (e.g., Moq) to define expected method calls and return values according to the London School principles.

## 6. AI Verifiable Completion Criteria for this Plan

The AI Verifiable Outcome for this task is the creation of this Test Plan document at [`docs/test_plans/WalletFramework.Core_test_plan.md`](docs/test_plans/WalletFramework.Core_test_plan.md). The criteria for verifying the completion of *this plan document itself* are:

1.  The file [`docs/test_plans/WalletFramework.Core_test_plan.md`](docs/test_plans/WalletFramework.Core_test_plan.md) exists.
2.  The file contains Markdown formatted content.
3.  The content includes sections for Introduction, Test Scope, Test Strategy, Granular Test Cases, and Test Data/Mock Configurations.
4.  The "Test Scope" section explicitly links to relevant AI Verifiable End Results from `PRDMasterPlan.md`.
5.  The "Test Strategy" section describes the adoption of London School TDD principles (behavior focus, mocking, outcome verification) and a recursive testing strategy (triggers, prioritization, selection).
6.  The "Granular Test Cases" section lists specific test cases for `WalletFramework.Core` functionalities.
7.  Each test case in the "Granular Test Cases" section includes descriptions for: Unit Under Test, Relevant AI Verifiable End Result, Interactions to Test, Collaborators to Mock/Stub, Observable Outcome Verification, Recursive Scope, and AI Verifiable Completion Criterion.
8.  Every test case defined has a clearly stated AI Verifiable Completion Criterion, typically referencing expected test runner output (e.g., "Test method `MethodName_Scenario_ExpectedOutcome` passes").
9.  The "Test Data and Mock Configurations" section provides guidance on necessary test data and mock setups.

## 7. Conclusion

This granular test plan for `WalletFramework.Core` provides a detailed blueprint for implementing tests that adhere to London School TDD principles, directly verify components contributing to AI Verifiable End Results from the Master Project Plan, and are integrated into a robust recursive testing strategy. This plan ensures that the foundational `Core` module is thoroughly tested for correctness and stability throughout the development lifecycle, supporting the successful implementation and verification of higher-level features. The module is now ready for test code implementation based on this plan.