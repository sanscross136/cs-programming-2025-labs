shifts = [6, 12, 8, 24, 10, 4]

sorted_shifts = list(filter(lambda shift: shift >= 8 and shift <= 12, shifts))

print(sorted_shifts)