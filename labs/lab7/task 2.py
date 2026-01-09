staff_shifts = [
    {"name": "Dr. Shaw", "shift_cost": 120, "shifts": 15},
    {"name": "Agent Torres", "shift_cost": 90, "shifts": 22},
    {"name": "Researcher Hall", "shift_cost": 150, "shifts": 10}
]
max_employee = max(staff_shifts, key=lambda x: x["shift_cost"] * x["shifts"])

print(f"Максимальная стоимость: {max_employee['name']}: {max_employee['shift_cost'] * max_employee['shifts']}")