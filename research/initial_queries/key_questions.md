# Key Research Questions

Based on the "Deep & Meaningful Tests to Include" section of the User Blueprint, the following key questions will guide the research:

## Edge-Case Functional Tests
- What are common edge cases for handling empty, null, or oversized payloads in .NET applications, specifically within the context of JSON and URI processing?
- What constitutes an "invalid credential configuration" in the context of the wallet framework, and what specific invalid configurations should be tested?

## Concurrency & Thread-Safety
- What are the potential concurrency issues and race conditions that can occur during parallel wallet record operations in an in-memory store?
- How can race conditions be specifically tested and identified in the `PaymentTransactionDataSamples` or similar components?

## Negative & Security-Focused Tests
- What are the standard methods for testing against tampered JSON Web Tokens (JWTs) and replayed HTTP requests in a .NET environment?
- What are the best practices and common vulnerabilities related to CSRF and XSS in cookie-based authentication flows, and how can they be tested?
- What are the requirements and implications of using a FIPS-compliant Random Number Generator (RNG) for encryption/decryption flows?
- What are the specific edge cases for SD-JWT selective disclosure, particularly with maximum nested claims, and how can these be tested for compliance?

## Performance Benchmarks
- What are effective strategies and tools for benchmarking bulk serialization and deserialization performance in .NET?
- How can a high-throughput credential issuance simulation be designed and implemented for performance testing?

## Compliance Scenarios
- What are the key compliance requirements related to cryptography in decentralized identity and wallet frameworks?
- What are the specific compliance aspects of SD-JWT that need to be verified through testing?