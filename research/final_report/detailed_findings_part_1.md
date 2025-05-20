# Detailed Findings - Part 1

This document presents the detailed findings gathered during the research process, organized by the specified deep testing areas.

## Edge-Case Functional Tests

### Primary Findings

-   .NET's `System.Text.Json` offers configurations for JSON serialization/deserialization, including case-insensitivity and null handling.
-   `JsonException` is thrown for certain invalid JSON formats during deserialization.
-   URI handling behavior in .NET is configurable via `runtimeconfig.json` or project files.
-   *Knowledge Gap:* Specifics on handling oversized payloads for JSON/URIs and defining invalid credential configurations within the wallet framework are missing.

### Secondary Findings

-   General .NET configuration using JSON files (`appsettings.json`, etc.) is relevant for configuring serialization and URI handling.
-   Basic .NET CLI commands (`dotnet restore`, `dotnet test`) and testing frameworks (MSTest) provide the environment for implementing edge-case tests.
-   GitHub Actions can automate testing workflows.

### Expert Insights

-   Disabling `TypeNameHandling` in `JsonSerializer` is a security best practice.
-   Testing frameworks offer configuration options for test execution.

## Concurrency & Thread-Safety

### Primary Findings

-   .NET provides synchronization primitives (`Barrier`, `SemaphoreSlim`, etc.) and concurrent collections (`ConcurrentDictionary`, `ConcurrentQueue`, etc.).
-   Thread-safe practices include using `lock` and `Interlocked.CompareExchange`.
-   Unsafe access to non-thread-safe objects and improper synchronization can lead to deadlocks and data corruption.
-   Concurrency Visualizer is available for profiling.
-   *Knowledge Gap:* Specific strategies for testing parallel wallet operations against an in-memory store and race conditions in `PaymentTransactionDataSamples` are missing.

### Secondary Findings

-   TPL (`Parallel.Invoke`, `Parallel.For`) and PLINQ are available for parallel execution.
-   Parallel loops can be cancelled with `CancellationToken`.
-   Lazy initialization can be used with parallel computation.
-   ETW events track thread pool activity.
-   Project file properties can control parallelism in some build scenarios.

### Expert Insights

-   Utilize thread-safe collections and judiciously employ synchronization primitives.
-   Avoid unsafe access to non-thread-safe objects and guard against race conditions.
-   Be aware of deadlock potential and use atomic operations for simple updates.
-   Leverage profiling tools for analysis.

## Negative & Security-Focused Tests

### Primary Findings

-   .NET (WCF) supports handling security tokens (SAML, Kerberos) and different security header patterns.
-   `System.Security.Cryptography.RandomNumberGenerator` should be used for secure random numbers.
-   Secure coding practices are needed to prevent injection, weak crypto, and insecure XML handling.
-   Certificate validation is important.
-   NuGet auditing helps identify vulnerabilities.
-   `dotnet dev-certs` manages development certificates.
-   *Knowledge Gaps:* Specific testing for tampered tokens, replayed requests, comprehensive CSRF/XSS, FIPS compliance steps, and SD-JWT selective disclosure edge cases are missing.

### Secondary Findings

-   WCF security protocols and message security concepts are relevant.
-   Preventing XML external entity attacks requires secure XML processing.
-   Certificate management and validation are important for secure communication.
-   .NET code analysis rules help identify security vulnerabilities.

### Expert Insights

-   Use cryptographically secure RNG and avoid weak cryptography.
-   Sanitize user input and secure XML processing.
-   Validate certificates and use enumeration names for security protocols.
-   Leverage security auditing tools.

## Performance Benchmarks

### Primary Findings

-   Various .NET serialization methods exist (`System.Text.Json`, `XmlSerializer`, `DataContractSerializer`).
-   `XmlSerializerGenerator` can improve XML serialization startup.
-   `DeserializeAsyncEnumerable` supports streaming deserialization.
-   Collection and string operation choices impact performance.
-   *Knowledge Gaps:* Specifics on benchmarking bulk serialization/deserialization (1000 records) and simulating high-throughput issuance are missing.

### Secondary Findings

-   Different serialization methods have varying performance.
-   Performance optimization techniques and streaming for large data are available.
-   Collection types and string manipulation impact performance.

### Expert Insights

-   Choose appropriate serialization methods and optimize startup.
-   Employ streaming for large data.
-   Consider collection performance and benchmark critical operations.

## Compliance Scenarios

### Primary Findings

-   .NET provides classes for digital signatures, public-key encryption, and hashing.
-   RSA padding and digest support vary across platforms.
-   FIPS mode behavior can be configured.
-   Custom cryptography can be configured.
-   Data classification and redaction features are available.
-   *Knowledge Gaps:* Specific OID4VC, mDoc, SD-JWT cryptographic compliance details, FIPS compliance verification for the wallet framework, and SD-JWT selective disclosure compliance aspects are missing.

### Secondary Findings

-   .NET cryptography primitives and algorithms are available.
-   Configuration options exist for cryptographic behavior and FIPS mode.
-   Cross-platform cryptography support needs consideration.
-   Data classification and redaction assist with compliance.

### Expert Insights

-   Configure strong cryptography and use recommended classes.
-   Understand cross-platform support and manage FIPS mode.
-   Leverage data classification and redaction.