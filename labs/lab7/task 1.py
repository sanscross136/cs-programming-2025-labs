objects = [
    ("Containment Cell A", 4),
    ("Archive Vault", 1),
    ("Bio Lab Sector", 3),
    ("Observation Wing", 2)
]

objects = sorted(objects, key= lambda item: item[1])
print(objects)