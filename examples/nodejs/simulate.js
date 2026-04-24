const axios = require('axios');

// Configure your API Base URL
const API_BASE_URL = 'https://quantslot-api.onrender.com/api';

async function testEngine() {
    console.log("🎰 Connecting to QuantSlot Math Engine...");
    
    try {
        // 1. Check API Health
        console.log("Checking API health...");
        const healthRes = await axios.get(`${API_BASE_URL}/health`);
        console.log(`Health Status: ${healthRes.data.status} at ${healthRes.data.timestamp}\n`);

        // 2. Run a Simulation
        const spins = 100000;
        const bet = 10;
        console.log(`Running simulation for ${spins} spins at bet ${bet}...`);
        console.log(`This might take a few seconds...\n`);

        const startTime = Date.now();
        const response = await axios.post(`${API_BASE_URL}/simulate`, {
            spins: spins,
            bet: bet
        });
        const elapsed = (Date.now() - startTime) / 1000;

        const results = response.data.results;
        
        console.log("✅ Simulation Completed!");
        console.log(`⏱️  Time Elapsed: ${elapsed} seconds`);
        console.log("-----------------------------------------");
        console.log(`📊 Verified RTP:          ${results.rtpPercentage}%`);
        console.log(`🎯 Hit Frequency:         ${results.hitFrequencyPercentage}%`);
        console.log(`💰 Max Win Multiplier:    ${results.maxWinMultiplier}x`);
        console.log(`🎰 Total Won / Total Bet: ${results.totalWon} / ${results.totalBet}`);
        console.log(`🎁 Bonus Triggers:        ${results.bonusTriggerFrequency}`);
        console.log("-----------------------------------------");

    } catch (error) {
        console.error("❌ Error connecting to the API:", error.response ? error.response.data : error.message);
    }
}

testEngine();
