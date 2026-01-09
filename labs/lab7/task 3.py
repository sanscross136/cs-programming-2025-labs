personnel = [
    {"name": "Dr. Klein", "clearance": 2},
    {"name": "Agent Brooks", "clearance": 4},
    {"name": "Technician Reed", "clearance": 1}
]

AccessLevels = {1: 'Restricted', 2: 'Confidential', 
                3: 'Confidential', 4: 'Top Secret'} 

personnel = list(map(lambda person: {**person, "category": AccessLevels[person["clearance"]]},personnel))

print(personnel)