# QuantSlot Math Engine & RTP API 🎰

![Node.js](https://img.shields.io/badge/Node.js-v18+-green.svg)
![Service](https://img.shields.io/badge/Service-API_as_a_Service-blue.svg)
![Status](https://img.shields.io/badge/Status-Live-brightgreen.svg)

**QuantSlot API** is an enterprise-grade, high-performance mathematical engine designed for the iGaming industry. We provide a robust, cloud-based stochastic simulation backend so you can focus on building stunning frontend slot games without worrying about the complex math.

Whether you are building a *Cluster-Pay* slot with infinite cascades, or calculating the exact RTP and volatility of a new mechanic, QuantSlot provides the precision and speed required to meet international certification standards (GLI, iTech Labs).

---

## 🚀 Why Choose QuantSlot?

*   **Plug-and-Play Math:** Connect your frontend to our secure engine. We handle the probability, you handle the graphics.
*   **Complex Mechanics Ready:** Native support for 5x5 Cluster Wins, Cascading replacements, Multi-tier Bonuses, and dynamic multipliers.
*   **Lightning Fast Monte Carlo:** Simulate millions of spins in seconds to validate your game's RTP before production.
*   **CSPRNG Security:** Core random number generation utilizes cryptographic standards to ensure provably fair outcomes.
*   **Secure & Proprietary:** To protect our core intellectual property and math models (industry standard), the engine source code is maintained in a private secure vault. This documentation serves as the public interface.

---

## 📡 API Integration Guide

Our API operates over standard HTTP POST requests, returning fast, structured JSON responses.

### Base URL
> `https://quantslot-api.onrender.com/api`

---

### Endpoint: `/health`

A lightweight endpoint to verify the API service is up and running. Useful for monitoring and load balancers.

*   **Method:** `GET`

**Example Request:**
```bash
curl https://quantslot-api.onrender.com/api/health
```

**Example Response:**
```json
{
  "status": "ok",
  "timestamp": "2026-04-24T02:12:40.895Z"
}
```

---

### Endpoint: `/simulate`

Executes a mathematical simulation over a specified number of spins to generate accurate RTP and gameplay metrics.

*   **Method:** `POST`
*   **Headers:** `Content-Type: application/json`

#### Request Payload

| Field | Type | Description | Required | Default |
| :--- | :--- | :--- | :--- | :--- |
| `spins` | Integer | Number of spins to simulate (Max: 10,000,000). | Yes | 10,000 |
| `bet` | Integer | Base bet amount per spin. | Yes | 10 |

**Example Request:**
```bash
curl -X POST https://quantslot-api.onrender.com/api/simulate \
     -H "Content-Type: application/json" \
     -d '{"spins": 100000, "bet": 10}'
```

#### JSON Response

The API returns detailed metrics including total wagered, total won, exact RTP percentage, hit frequency, and bonus trigger rates.

**Example Response:**
```json
{
  "status": "success",
  "simulation": {
    "spinsRequested": 100000,
    "betAmount": 10,
    "timeElapsedSeconds": 1.24
  },
  "results": {
    "totalBet": 1000000,
    "totalWon": 964250,
    "rtpPercentage": "96.4250",
    "hitFrequencyPercentage": "18.42",
    "maxWinAmount": 25000,
    "maxWinMultiplier": "2500.00",
    "bonusTriggerFrequency": "1 in 215 spins",
    "featureTriggerFrequency": "1 in 150 spins"
  }
}
```

---

## 💻 Code Examples

### JavaScript (Fetch)
```javascript
const getRTPMetrics = async () => {
  const response = await fetch('https://quantslot-api.onrender.com/api/simulate', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ spins: 50000, bet: 5 })
  });
  
  const data = await response.json();
  console.log(`Verified RTP: ${data.results.rtpPercentage}%`);
};
```

### Python (Requests)
```python
import requests

url = "https://quantslot-api.onrender.com/api/simulate"
payload = {
    "spins": 50000,
    "bet": 5
}
headers = {"Content-Type": "application/json"}

response = requests.post(url, json=payload, headers=headers)
data = response.json()

print(f"Hit Frequency: {data['results']['hitFrequencyPercentage']}%")
```

---

## 🤝 Licensing & Commercial Use

QuantSlot API is currently available for integration and testing. If you are a game studio or developer looking to license this math engine for a commercial production environment, please contact the lead engineer.

**Developed by bmontes93** - *Senior Software Engineer & iGaming Math Specialist.*
