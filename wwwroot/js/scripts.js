
function showFact(id, text) {
    document.getElementById("fact-id").innerText = id;
    document.getElementById("fact-text").innerText = text;
}

var facts = [];
async function loadFacts() {
    let response = await fetch("/Facts/GetFacts?count=50");

    if (response.ok) {
        facts = await response.json();
    } else {
        alert("Ошибка HTTP: " + response.status);
    }
}

async function showNextFact() {
    if (facts.length == 0) {
        await loadFacts();
    }

    fact = facts.pop();
    showFact(fact.id, fact.text);
}

showNextFact();