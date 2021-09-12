# Brain
Application that consists of a few small games that help improve your cognitive functioning (meant for fun, nothing is scientifically proven)
The score is evaluated and stored for each game after every sesion. There is a special section called statistics that shows users'
progress over time.

## How one session works:
When the program is run, 2 things appear, “Enter user” and “Add new
user”. If a correct, already existing name is written, all data about that user is
pulled from the text file previously-stored (and all new progress of the user will be added there
as well). If the new user option is picked, it will create a new file with the username where all
future data is stored.
In both cases, the next window (main menu) will show the list of all games 
and a statistics button which, when clicked, shows the progress chart for each
game.
If one of the games is clicked, the game starts. Under every game button there exists a "?" button
where all of the rules are explained.

The following games are available:

### 1) Path finding
10 puzzles appear per session. One puzzle will be the following: for a short amount of time, a
grid is shown with mines on some of the places. After that, the mines get
hidden, and staring and ending positions appear (guaranteed to be on the “safe” spots). 
The players' job is to connect them by choosing a path without any mines. If it is done successfully, the puzzle is added to
the final score. Every grid is worth a different amount of points, and all the grids and the mine
spots are produced at random (To make the game a bit easier, there are at least 3 different
ways to connect the dots – this is checked with a path finding algorithm).

### 2) Sum up
This game is timed by 1 minute, and the score is evaluated based on the number of correct
answers. A number is shown in top of the screen, and a grid of some lower valued numbers is
drawn below. The player is supposed to click on the grid selecting the numbers that sum up
to the top one. If the sum exceeds the desired value, the round is marked as wrong.

### 3) From low to high
This game is timed by 1 minute, and the score is evaluated based on the number of correct
answers. The game is simple, some amount of random integer number (range [-100,100]), dots, 
or roman numbers are shown on the screen. The player simply needs to click on them from the descending
order. If a mistake is made, the answer is marked as incorrect and the next few values are
shown.

### 4) Partial match 
This game is timed by 1 minute, and the score is evaluated based on the number of correct
answers. The way it works is that the player needs to decide is if the picture currently visible is
the same, different, or partially different from the previous picture. Partially being if the shape is
the same, but the color isn’t, or vice versa. The decision is made by clicking the left (no), down
(partially), or right(yes) arrow key. The choice of both will be random.
