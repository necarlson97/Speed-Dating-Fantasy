# Run a batch of GPT3 prompts
# used for ludum dare #51 - "Every 10 Seconds" for "Fantasy Speed Dating"
import os
import openai
import random

species = {
    "minotaur": None,
    "fairy": None,
    "angel": None,
    "devil": None,
    "elf": "fantasy elf",
    "dwarf": "fantasy dwarf",
    "zombie": None,
    "werewolf": None,
    "vampire": None,
    "orc": None,
}

jobs = [
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
]

personalities = [
  "Comedic",
  "Kindly",
  "Shy",
  "Dasdardly",
  "Proud",
  "Wise",
  "Fiersome",
  "Simple",
  "Artistic",
  "Serious"
]

openai.api_key = "OPEN-AI-KEY"


for species, species_prompt in species.items():
  # Just to keep equal number of personalities
  shuffled = sorted(personalities, key=lambda k: random.random())
  for job in jobs:
      personality = shuffled.pop()
      species_prompt = species_prompt or species
      prompt = f"{personality} {job} {species_prompt}"
      file_name = f"{species}_{job}_{personality}"
      response = openai.Completion.create(
        model="text-davinci-002",
        prompt=f"I met a {prompt}, they talked about their life:\n\"",
        temperature=0.7,
        max_tokens=128,
        top_p=1,
        frequency_penalty=0,
        presence_penalty=0,
        n=3,
      )
      text = ""
      for choice in response.choices:
        text += choice["text"] + "\n\n"
      with open(f'./bio/{file_name}.txt', 'w') as f:
        f.write(text)
      print(f"Written to {file_name}")