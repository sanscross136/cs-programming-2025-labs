evaluations = [
    {"name": "Agent Cole", "score": 78},
    {"name": "Dr. Weiss", "score": 92},
    {"name": "Technician Moore", "score": 61},
    {"name": "Researcher Lin", "score": 88}
]

best = max(evaluations, key=lambda x: x["score"])

print(f"{best['name']}: {best['score']}")