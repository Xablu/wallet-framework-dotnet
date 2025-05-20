# High-Level Acceptance Test A-06: Dynamic Application Security Testing (DAST)

## Description

This high-level acceptance test verifies that the running Wallet Framework application is free from critical and high-risk security vulnerabilities by performing dynamic analysis. DAST tools interact with the application in a running state to identify potential weaknesses.

## AI Verifiable Success Criterion

The test passes if the Continuous Integration (CI) pipeline includes a step to run a Dynamic Application Security Testing (DAST) scan (e.g., using OWASP ZAP) against a running instance of the application, and the generated scan report indicates zero critical or high-risk vulnerabilities.

**Verification Steps (for AI):**

1.  Deploy and start a test instance of the Wallet Framework application in the CI environment.
2.  Execute the DAST scan tool (e.g., OWASP ZAP) targeting the running application instance.
3.  Generate the DAST scan report in a machine-readable format (e.g., JSON or XML).
4.  Parse the report to identify vulnerabilities and their severity levels.
5.  Check if the report contains any vulnerabilities classified as "Critical" or "High" risk.
6.  If no critical or high-risk vulnerabilities are found, the test passes. Otherwise, the test fails.

## Rationale

DAST complements SAST by identifying vulnerabilities that can only be detected when the application is running, such as authentication issues, injection flaws, and misconfigurations. Including DAST in the CI pipeline provides an essential layer of security validation for the deployed application.