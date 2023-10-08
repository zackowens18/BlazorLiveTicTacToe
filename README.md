# BlazorLiveTicTacToe

A simple live TicTacToe implementation using blazor. Communicates using SignalR.

## Project layout

- Client
  - Holds all of the blazor front end client. Uses CSS, HTML, and Blazor
- Server
  - Holds all of the server code. Uses SignalR to commmunicate with the client
- Shared
  - Holds objects that are used to in both server and client like GameData

## Scope

This project has the following features:

1. User can join a room to play. First player to join is X.
2. After 2 users join a room no more can.
3. Users can play Tic Tac Toe and a popup occurs with the results.
4. Any user can leave and join if no other user takes the place in the meantime
