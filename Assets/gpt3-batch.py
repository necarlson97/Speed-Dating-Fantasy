# Run a batch of GPT3 prompts
# used for ludum dare #51 - "Every 10 Seconds" for "Fantasy Speed Dating"
import os
import openai
import random

characters = [
  "angel_Builder_Fearsome",
  "devil_Merchant_Fearsome",
  "dwarf_Tailor_Fearsome",
  "elf_Sailor_Fearsome",
  "fairy_Musician_Fearsome",
  "minotaur_Baker_Fearsome",
  "orc_Teacher_Fearsome",
  "vampire_Teacher_Fearsome",
  "werewolf_Teacher_Fearsome",
  "undead_Musician_Fearsome",
]

openai.api_key_path  = "./openai-key.txt"


for character in characters:
    species, job, personality = character.split('_')
    
    prompt = f"{personality} {job} {species}"
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
    with open(f'./{file_name}.txt', 'a+') as f:
      f.write(text)
    print(f"Written to {file_name}")