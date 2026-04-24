import requests
import time

API_BASE_URL = "https://quantslot-api.onrender.com/api"

def test_engine():
    print("🎰 Connecting to QuantSlot Math Engine...")
    
    try:
        # 1. Check API Health
        print("Checking API health...")
        health_res = requests.get(f"{API_BASE_URL}/health")
        health_res.raise_for_status()
        health_data = health_res.json()
        print(f"Health Status: {health_data['status']} at {health_data['timestamp']}\n")

        # 2. Run a Simulation
        spins = 100000
        bet = 10
        print(f"Running simulation for {spins} spins at bet {bet}...")
        print("This might take a few seconds...\n")

        payload = {
            "spins": spins,
            "bet": bet
        }
        
        start_time = time.time()
        response = requests.post(f"{API_BASE_URL}/simulate", json=payload)
        response.raise_for_status()
        elapsed = time.time() - start_time
        
        data = response.json()
        results = data['results']

        print("✅ Simulation Completed!")
        print(f"⏱️  Time Elapsed: {elapsed:.2f} seconds")
        print("-" * 41)
        print(f"📊 Verified RTP:          {results['rtpPercentage']}%")
        print(f"🎯 Hit Frequency:         {results['hitFrequencyPercentage']}%")
        print(f"💰 Max Win Multiplier:    {results['maxWinMultiplier']}x")
        print(f"🎰 Total Won / Total Bet: {results['totalWon']} / {results['totalBet']}")
        print(f"🎁 Bonus Triggers:        {results['bonusTriggerFrequency']}")
        print("-" * 41)

    except requests.exceptions.RequestException as e:
        print(f"❌ Error connecting to the API: {e}")

if __name__ == "__main__":
    test_engine()
