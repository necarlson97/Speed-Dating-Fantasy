# # Simple script to help renaming portraits and whatnot


# Get-ChildItem "./Resources/old-portraits" -Filter *.png | Foreach-Object {
#     $oldFileName = $_.Basename

#     Replace any of the overly wordy ones
#     $prompt = $oldFileName.replace("fantasy_elf","elf")
#     $prompt = $prompt.replace("fantasy_dwarf","dwarf")
#     $prompt = $prompt.replace("devil_with_red_skin","devil")
#     $prompt = $prompt.replace("orc_with_green_skin","orc")

#     # Swap around order of words
#     $personality, $job, $species = $prompt.Split('_')
#     $newFileName = $species + "_" + $job + "_" + $personality
    
#     $oldFile = "./Resources/old-portraits/" + $oldFileName + ".png"
#     $newFile = "./Resources/portraits/" + $newFileName + ".png"
#     echo $oldFile
#     echo $newFile
#     echo ""
    
#     Move-Item -Path $oldFile -Destination $newFile
# }

# Simmilar but slightly different goals here:
# Get-ChildItem "./Resources/portraits" -Filter *.png | Foreach-Object {
#     $oldFileName = $_.Basename

#     # Replace some words
#     $newFileName = $oldFileName.replace("fantasy_elf","elf")
#     $newFileName = $newFileName.replace("fantasy_dwarf","dwarf")
#     $newFileName = $newFileName.replace("devil_with_red_skin","devil")
#     $newFileName = $newFileName.replace("orc_with_green_skin","orc")
#     $newFileName = $newFileName.replace("zombie","undead")
    
#     $oldFile = "./Resources/portraits/" + $oldFileName + ".png"
#     $newFile = "./Resources/portraits/" + $newFileName + ".png"
#     echo $oldFile
#     echo $newFile
#     echo ""
    
#     Move-Item -Path $oldFile -Destination $newFile
# }

# Simmilar but slightly different goals here:
Get-ChildItem ".\Resources\bio\" | Foreach-Object {
    $oldFileName = ".\Resources\bio\" + $_.Basename + $_.Extension

    # Replace some words
    $newFileName = $oldFileName.replace("fantasy_elf","elf")
    $newFileName = $newFileName.replace("fantasy_dwarf","dwarf")
    $newFileName = $newFileName.replace("devil_with_red_skin","devil")
    $newFileName = $newFileName.replace("orc_with_green_skin","orc")
    $newFileName = $newFileName.replace("zombie","undead")

    echo "Old: $oldFileName"
    echo $newFilename
    echo ""

    Move-Item -Path $oldFileName -Destination $newFilename
}