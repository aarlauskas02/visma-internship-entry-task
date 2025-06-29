# How to run

1. Run the program once for it to generate the folder for the JSON files in your ..\AppData\Roaming folder.
2. Move the example JSON files to the newly created ShortageManager folder. This is necessary to log in and properly run the unit tests.
3. Run the program again and log in using either TestAdmin or TestRegular.
4. Follow the displayed instructions.

# Further improvements

Due to time limits, or since the problem was noticed in reflection after coding, I was not able to implement certain things. These are possible future improvements for the code:

1. Documentation. Even though the code is quite self-explanatory since the task is rather simple, in a larger project context thorough documentation would be important to include.
2. Better authorization. The requirements didn't specify how robust the login must be so I went for the simplest option. Apart from obvious security extensions, the user class could be made into an interface with different implementations depending on the user type if there was need for more specialization.
3. Purpose seperation. Even though the program is seperated into files with a single purpose, a different design pattern could have been chosen to make it more maintainable.
4. Unit testing. Whilst the unit tests cover most of the functionality, they could be more thorough with testing certain edge cases.
