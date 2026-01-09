scp_objects = [
    {"scp": "SCP-096", "class": "Euclid"},
    {"scp": "SCP-173", "class": "Euclid"},
    {"scp": "SCP-055", "class": "Keter"},
    {"scp": "SCP-999", "class": "Safe"},
    {"scp": "SCP-3001", "class": "Keter"}
]

usilenniy_containment = list(filter(lambda scp_object: scp_object['class'] != 'Safe',scp_objects))
# я мог написать reinforced, но я русский
print(usilenniy_containment)