This is a simple project made in a handful of hours to practice with a state machine, showcasing a person in a house with the player controlling what devices around the house are broken.

You are welcome to use this project however you would like.

There are 5 states:

Patrol

-Patrols between five different locations spread out across the rooms

-If at any point it sees a broken object, it goes to the Fix Object state

-After 10 seconds, if it doesn’t see any broken objects, it will randomly go to the Sleep or Work state.


Fix Object

-Goes to the object it needs to fix

-After a few seconds, the object is fixed then…

---If it’s been less than 30 seconds since fixing another object, go to the Anger state

---If it’s been more than 30 seconds since fixing another object, 

-----If entered from the Work state, when it is done being fixed, it will return immediately to the Work state instead of the Patrol state

-----If not entered from the Work state, go to the Patrol state

Sleep

-Walks to the bed, then waits there for a few seconds before returning to Patrol

-It will not fix broken objects it sees on the way to the bed

Work

-Walks to the computer, then waits there for a few seconds before returning to Patrol. However, it has a chance of going to the Anger state instead

---However, if the computer is broken, it will enter the Fix Object state for it. After the fix state, it will return immediately to the Word state instead of the Patrol state.

-It will not fix any other broken objects it sees, only the computer

Anger

-Material changes red and it stands where it is for a few seconds

-After a few seconds, it goes to the Sleep state
