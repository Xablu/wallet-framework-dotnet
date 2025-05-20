# Initial Strategic Research Report

## 1. Executive Summary  
This report provides the strategic research foundation for building a **high-velocity**, **secure**, and **comprehensive** test framework for **wallet-framework-dotnet**. It outlines key objectives, research scope, competitive landscape, risk assessment, and actionable recommendations to guide the SPARC cycle and Master Project Plan.

---

## 2. Background & Context  
- **Project:** wallet-framework-dotnet  
- **Domain:** Decentralized identity – OpenID for Verifiable Credentials, mDoc, SD-JWT, Hyperledger Aries  
- **Current State:** Modular C# code-base with partial test coverage; missing test project files; no unified CI pipeline for SAST/DAST/SCA or performance benchmarking  
- **Strategic Imperative:** Deliver a test framework that ensures functional correctness, enforces OWASP security standards, and provides rapid feedback in CI.

---

## 3. Research Objectives  
1. **Assess** existing test tooling and best practices in .NET (xUnit, Moq, FsCheck, SpecFlow)  
2. **Benchmark** performance testing solutions for serialization, ledger interactions, and cryptographic operations  
3. **Evaluate** static & dynamic security scanning integrations (Roslyn, OWASP ZAP, Dependency-Check)  
4. **Survey** CI/CD approaches for parallel execution and matrix builds on GitHub Actions  
5. **Identify** gaps and opportunities to differentiate our framework in terms of speed, coverage, and security rigor  

---

## 4. Scope & Methodology  
- **Literature Review:**  
  - xUnit.net parallel execution & coverage tools (Coverlet, ReportGenerator)  
  - SpecFlow + BrowserStack cross-browser BDD pipelines  
  - FsCheck property-based testing patterns in C#  
- **Competitive Analysis:**  
  - Compare open-source .NET testing frameworks (NUnit, MSTest) and third-party commercial offerings  
  - Analyze similar decentralized identity projects for their test practices  
- **Technical Prototyping:**  
  - Create minimal sample test harnesses for serialization speed (System.Text.Json vs. Newtonsoft.Json)  
  - Run OWASP ZAP against a stubbed WebApplicationFactory endpoint  
  - Execute parallel test suites on matrix of .NET versions  
- **Stakeholder Interviews:**  
  - Developers and security engineers at Xablu  
  - Operations team for CI infrastructure requirements  

---

## 5. Competitive & Landscape Analysis  
| Framework       | Strengths                                   | Weaknesses                                |
|-----------------|---------------------------------------------|-------------------------------------------|
| **xUnit.net**   | Native parallelization, flexible fixtures   | Limited out-of-the-box BDD support        |
| **NUnit**       | Mature ecosystem, parameterized tests       | Slower startup, less CI-friendly by default |
| **SpecFlow**    | Native Gherkin, strong .NET integration     | Steeper learning curve, slower E2E runs   |
| **FsCheck**     | Powerful property testing, integrates with xUnit | Harder to debug counterexamples        |

- **Security Scanning Tools:**  
  - **Roslyn Analyzers:** Simple CI integration, high false-positive filtering required  
  - **OWASP ZAP:** Robust dynamic scanning, requires headless or containerized deployment  
  - **Dependency-Check:** Broad CVE coverage but heavy initial configuration  

---

## 6. Key Findings & Gaps  
1. **Fragmented Test Suites:** Multiple `*.Tests` projects exist, but no unified solution file or CI orchestration  
2. **Security Scans Absent:** No automated DAST/SCA; only partial static analysis in code  
3. **Performance Blind Spots:** No benchmarks for serialization, ledger interactions, or cryptographic primitives  
4. **Limited Property Testing:** Functional edge-cases not exhaustively exercised by random inputs  
5. **End-to-End Pipeline:** Lack of cross-browser BDD confirmation in current CI  

---

## 7. Risk & Opportunity Assessment  
- **Risks:**  
  - Slow test suite discourages developer adoption  
  - Undetected security vulnerabilities in test code or dependencies  
  - Fragmented CI leads to coverage gaps  
- **Opportunities:**  
  - Establish a “gold standard” .NET test framework for decentralized identity libraries  
  - Use parallel and matrix CI to reduce feedback time < 2 minutes for unit suite  
  - Leverage property-based testing to uncover subtle defects early  

---

## 8. Strategic Recommendations  
1. **Consolidate Tests into a Single Solution** (`wallet-framework-dotnet.Tests.sln`) for streamlined CI.  
2. **Adopt xUnit + Moq + Coverlet** as the primary unit-test stack; enable default parallel execution.  
3. **Integrate SpecFlow + BrowserStack** for top-level BDD flows (`IssueCredential`, `PresentProof`).  
4. **Embed Security Scans**:  
   - Roslyn analyzers at “error” level in `Directory.Build.props`  
   - OWASP ZAP step against in-memory WebApplicationFactory host  
   - Dependency-Check with gating on CVE ≥ 7.0  
5. **Enable FsCheck** for core parsing/validation modules with a minimum of 200 random cases each.  
6. **Benchmark & Automate Performance Tests** using a lightweight harness (BenchmarkDotNet) for serialization and ledger loops.  
7. **Design CI Matrix**: .NET
