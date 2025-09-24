using System.Collections.Generic;


public class DialogueBlock
{
    private List<KeyValuePair<string, float>> dialoguesAndTimes = new();
    private float dialogueDuration = 3.8f; // default time for all dialogues

    public DialogueBlock(string planetName)
    {
        switch (planetName)
        {
            case "1": // Observatory start
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Hello, student!", 2.5f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "You've come to learn the secrets of alchemy? To create gold?", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Well, that's wonderful!", 2f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "There's just one small problem however...", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Even I, the ever-humble Galileo Galilei, have absolutely no idea how to make real gold.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Before I get back to my research, let me show you what you'll be working on...", dialogueDuration));
                break;

            case "2": // Mines intro
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "In alchemy, we learn how to combine the elements of the universe in order to create gold.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "We get these elements here, in the mines.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "To get resources from these rocks, timing the swings of your pickaxe is key.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Try it yourself, see if you can find salt and sulfur. Press the mine button to start", dialogueDuration));
                break;

            case "3": // After digging salt & sulfur
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Nicely done, we'll be cooking up new elements in no time!", dialogueDuration));
                break;

            case "4": // Lab intro
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Now we'll use the cauldron here in the lab to combine these elements into something new!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "We do this by selecting two resources and cooking them together.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Try it out on the salt and sulfur that we just mined!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Start by adding them in the cauldron by clicking on the resource and then press the cook button.", 5f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "You can remove the item from the cauldron by clicking on the resource above it", dialogueDuration - 0.5f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "And if you cook resources that dont make a recipe you will loose those resources", dialogueDuration - 0.5f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "The inventory is scrollable and you can see how many recipies a resource is used in by hovering over it .", 5f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Oh make sure to keep track of your unique resources and recipies by writing on a piece of paper! ", 5f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "And lastly you can switch between scenes through the buttons on the left", 5f));
                break;

            case "5": // Ending
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Amazing! With this formula we will be the most respected alchemists in all the land!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Thank you student for helping me complete my life's work.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Know there is much more to discover in this world,", 3f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "I would be very impressed if you were able to create every possible element...", 5f));
                break;

            case "Moon": // Moon discovery
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Excellent work! You've just created your first titanium.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Now that you've been introduced to the basics, we can work on something more complicated.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "We've discovered two large bodies in the sky, we call them the Sun and Moon.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "The Sun is made of gold of course, but it's far out of reach.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "The Moon however has blessed the Earth with some of its silver.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "See if you can find silver in the mines!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Come back to the observatory once you've whipped up some new resources in the lab.", dialogueDuration));
                break;

            // --- Existing planets (Venus, Mars, Jupiter, Mercury, Saturn) ---
            case "Venus":
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Well done, I see you've been working hard.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "While you were playing in the lab, the grown-ups have discovered a new planet!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "We call it Venus, and it has gifted copper to Earth.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Go away and see if you can create some new and interesting compounds using it.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Remember to check the mines whenever we discover new resources.", dialogueDuration));
                break;

            case "Mars":
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Wow! This is some amazing stuff...", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "In the mean time I have made another discovery!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Another planet, smaller than the last, I call it Mars.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "You should be able to find iron in the mines now. I wonder if you can use it to create the wonderful red colour of the planet?", 5f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Leave now, we both have plenty of work to do.", dialogueDuration));
                break;

            case "Jupiter":
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "You're making such quick progress, by far my strongest student!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "I've been made aware of another planet, this one we will call Jupiter.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "It is far larger than any other ever discovered, and has given the Earth tin in abundance!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Return to the mines and work out the secrets of this new element.", dialogueDuration));
                break;

            case "Mercury":
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "We are discovering so many new resources thanks to you!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "My latest discovery is a small planet called Mercury, it is the closest one to the Sun.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "It gives us a piece of itself, metal mercury. Go and study it to advance science!", dialogueDuration));
                break;

            case "Saturn":
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "I believe this to be the final planet, a giant like Jupiter, with rings of gold. We're close!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "It has been called Saturn, and from it we have gotten lead.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "I can sense now that the philosopher's stone is almost upon us!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "What?", 2f));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "You don't know what the philosopher's stone is???", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "It's a fantastical crystalline substance that, when mixed with anything, will create gold!", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Be sure to focus all your efforts on finding this incredible material.", dialogueDuration));
                dialoguesAndTimes.Add(new KeyValuePair<string, float>(
                    "Come back here once you have made us rich!", dialogueDuration));
                break;
        }
    }

    public List<KeyValuePair<string, float>> GetDialogues()
    {
        return dialoguesAndTimes;
    }
}
