# TestTask.Minesweeper
Implementation of a test task for some company.

## Some notes
For errors: i used requried by api method: json object with single property "error". In my opinion, it's not machine-processable - need to add property like "ProbelmType".
Concurrency: Not used. One player plays in single game session. If need it - I can add it.

Integration tests: I can add them, if it's needed.

## How to start:

1. clone
2. cd TestTask.Minesweeper
3. docker compose -f docker-compose.yml -f docker-compose.override.yml up
4. put "https://localhost:5001" in textbox at "https://minesweeper-test.studiotg.ru/"
5. play