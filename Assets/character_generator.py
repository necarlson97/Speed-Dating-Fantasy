import random

# Just for fun, lets try creating some more combinations just to see
all_species = {
  "goblin": "goblin with green skin",
  "centaur": None,
  "cyclops": None,
  "lizardfolk": None,
  "dryad": None,
  "dragonborn": None,
  "water elemental": None,
  "fire elemental": None,
  "gnome": None,
  "mermaid": None,
  "faun": "faun with ram's horns",
  "merfolk": "merfolk with blue skin and gills",
}

jobs = {
  "Farmer",
  "Soldier",
  "Teacher",
  "Sailor",
  "Baker",
  "Builder",
  "Tailor",
  "Mayor",
  "Merchant",
  "Musician",
}

personalities = {
  "Comedic",
  "Kindly",
  "Shy",
  "Dasdardly",
  "Proud",
  "Wise",
  "Fearsome",
  "Simple",
  "Artistic",
  "Serious",
}
for species, species_prompt in all_species.items():
  # Just to keep equal number of personalities
  shuffled = sorted(personalities, key=lambda k: random.random())
  for job in jobs:
      personality = shuffled.pop()
      species_prompt = species_prompt or species
      prompt = f"{personality} {job} {species_prompt}"
      file_name = f"{species}_{job}_{personality}"
      print(file_name)
