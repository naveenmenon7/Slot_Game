# **Slot Machine**



Itch.io Link: https://naveenmenon.itch.io/slot-machine

GitHub Link: https://github.com/naveenmenon7/Slot\_Game



A small 2D slot machine game built in Unity 6.5 as part of a Unity game development assignment.



The project focuses on reel animation, randomized outcomes, betting, payout logic, UI interaction, and clean Unity project structure.







**Game Overview**



This is a simple three-reel casino-style slot machine.



The player starts with a balance of 1000 G and selects a bet of:



\- 10 G

\- 50 G

\- 100 G



After placing a bet, the player pulls the physical slot machine lever to spin all three reels.



The player wins when all three reels land on the same symbol.



**Winning Combinations**



Combination -- Payout



* 7 - 7 - 7 -- 10x bet
* Cherries - Cherries - Cherries -- 5x bet
* Bell - Bell - Bell -- 3x bet
* BAR - BAR - BAR -- 2x bet



For example:



A 50 G bet landing on 7 - 7 - 7 awards 500 G.



The bet is deducted when the round begins and the current bet resets after every round.







**Features**



1. Reel System



\- Three independent reels

\- Four symbols per reel

\- Continuous symbol looping

\- Smooth spinning and deceleration

\- Randomized reel stopping

\- Sprite masking to keep symbols inside the reel frame

\- Reel results are detected from the actual landed symbols



2\. Betting System



\- 10 G, 50 G and 100 G betting options

\- Starting balance of 1000 G

\- Bet is deducted when a spin begins

\- Current bet resets to 0 after each round

\- Player must place a new bet before every spin

\- Player can cancel an active bet before spinning

\- Betting is disabled while the reels are spinning



3\. Lever Interaction



\- Physical slot-machine lever used to start the game

\- Lever has a pull animation

\- Lever can only be used when a valid bet has been placed

\- Lever is locked while the reels are spinning



4\. Win and Payout System



\- Three matching symbols results in a win

\- Different symbols have different payout multipliers

\- Balance is updated automatically after each round

\- Win, loss and betting states are displayed through the UI



5\. UI



\- Current balance display

\- Current bet display

\- Bet selection buttons

\- Cancel bet button

\- Win/loss result display

\- Selected bet visual feedback

\- Disabled UI states while spinning







**Bonus Features**



The project does not currently include additional bonus symbols or complex bonus rounds.



Instead, additional polish has been added around the core slot-machine experience, including:



\- Animated physical lever

\- Bet selection feedback

\- Dynamic result colors

\- Multiple payout multipliers

\- Smooth reel movement

\- Betting and interaction state restrictions







**Thought Process / Approach**



The main goal was to build the slot machine around a simple and reliable gameplay loop rather than adding unnecessary complexity.



The core gameplay loop is:



1\. Player selects a bet.

2\. The selected bet is displayed and visually highlighted.

3\. Player pulls the lever.

4\. The bet is deducted from the balance.

5\. All three reels spin independently.

6\. Each reel stops on a randomized result.

7\. The three results are compared.

8\. If all three symbols match, the appropriate payout multiplier is applied.

9\. The result is displayed to the player.

10\. The current bet resets and the player must place a new bet for the next round.



The reel system was designed using reusable reel prefabs so that the same reel setup could be used for all three positions. Symbols continuously loop through the reel instead of creating unnecessary duplicated reel objects.



The project also separates responsibilities between different components. Reel behaviour is handled by the reel controller, symbol information is handled by the symbol component, lever interaction is handled separately, and the slot machine controller manages betting, spinning and payout logic.



The implementation prioritizes readable code, reusable components and straightforward game-state management.







**Controls**



**WebGL**



\- Select a bet using the on-screen betting buttons.

\- Click the lever to spin.

\- Click Cancel to cancel the current bet before spinning.



No keyboard controls are required.







**Running the WebGL Build**



The WebGL build is located in: Build/WebGL

