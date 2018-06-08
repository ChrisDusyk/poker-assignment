# Poker

Allows users to enter several players and hands into a Console GUI, and outputs the winner(s) based on Poker hand hierarchy.

## Technology

- Visual Studio 2017
- .NET Core 2.0 Console App
- MSTest unit testing project

### Source Code

- [GitHub repository](https://github.com/ChrisDusyk/poker-assignment)
- Zip sent in email

## Assumptions

- Ace is a high card
- No comparison between winning hands for anything but High Card
- No validation done to ensure all hands entered combine for a valid deck of cards (i.e. no duplicate cards)

### Poker Hand Hierarchy

Based on the assignment, the following hands are checked for:

- Flush
- Three of a Kind
- One Pair
- High Card

## Instructions

1. Start .exe or project in Visual Studio
2. Enter each player and hand in the following format: "[name], [card1], [card2], [card3], [card4], [card5]" (see Sample Data)
3. Press enter once the player and their hand have been entered to progress to next line, to input the next player
3. Once done entering all players and hands, enter a lower-case 'q' on the next line and press Enter

Sample data can also be tested in the Poker.Tests project, using Visual Studio's Test Explorer.

### Sample Data

- Joe, 2H, 4H, 5H, 6H, 8H
- Marge, 2D, 2S, JH, QD, KD
- Bob, 3C, 3D, 3S, 8C, 10D
- Sally, AC, 10C, 5C, 4D, 4S
- Devin, AD, 2D, 3D, 7S, 10H
- Maks, KH, QD, 10S, 9D, 7H

### Sample Output

![Output Sample](https://github.com/ChrisDusyk/poker-assignment/blob/master/Poker/Output%20Sample.png "Sample Output")