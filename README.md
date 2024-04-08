### Main Project - CarSimulation

#### 1. Project Overview
The CarSimulation project is a simulation system designed to manage the movement of cars on a field. It supports two types of simulations: single-car and multiple-cars. Users can input field dimensions, initial car positions, orientations, and sequences of commands for each car. The simulation then executes these commands and provides the final positions, orientations, and any collisions that occur.

#### 2. Architecture
The project follows a modular and object-oriented architecture, consisting of several components:

- **Main Program:** Entry point that initializes necessary handlers and orchestrates the simulation process.
- **Simulation Handlers:** Responsible for executing the simulation based on user input, with separate handlers for single-car and multiple-cars simulations.
- **Input Handlers:** Collect user input such as field size, car positions, and commands. Separate handlers exist for single-car and multiple-cars simulations.
- **Output Handlers:** Display simulation results to the user, also separated for single-car and multiple-cars simulations.
- **Car:** Represents a car in the simulation, including properties for position, orientation, and methods for executing commands.
- **Commands:** Define actions that a car can perform, such as moving forward, turning left, and turning right.
- **Collision Detector:** Detects collisions between cars during the simulation.
- **Field:** Represents the simulation field and provides methods for checking if positions are within bounds.

#### 3. Code Structure
```markdown

AutoDrivingCarSimulation
│   AutoDrivingCarSimulation.sln
│   
└── CarSimulation
    │   CarSimulation.csproj
    │   Program.cs
    │   
    ├── Commands
    │       MoveForwardCommand.cs
    │       TurnLeftCommand.cs
    │       TurnRightCommand.cs
    │       
    ├── Factory
    │       SimulationHandlerFactory.cs
    │       
    ├── Interfaces
    │       ICar.cs
    │       ICollisionDetector.cs
    │       ICommand.cs
    │       IInputHandler.cs
    │       IOutputHandler.cs
    │       ISimulationHandler.cs
    │       
    ├── Models
    │       Car.cs
    │       CarInput.cs
    │       Collision.cs
    │       Field.cs
    │       SimulationInput.cs
    │       
    ├── Simulation
    │       CollisionDetector.cs
    │       MultipleCarsSimulationHandler.cs
    │       SingleCarSimulationHandler.cs
    │       
    └── Utilities
            ├── Constants
            │       Constants.cs
            │       Orientation.cs
            │       
            ├── Extensions
            │       OrientationExtensions.cs
            │       
            ├── InputHandlers
            │       InputHandlerBase.cs
            │       MultipleCarsInputHandler.cs
            │       SingleCarInputHandler.cs
            │       
            └── OutputHandlers
                    MultipleCarsOutputHandler.cs
                    SingleCarOutputHandler.cs

```

#### 4. Design Principles Followed
- **Modular Design:** The project is divided into separate components/modules, promoting reusability and maintainability.
- **Object-Oriented Programming:** Classes and interfaces are used to represent real-world entities and encapsulate behaviors.
- **Single Responsibility Principle (SRP):** Each class is responsible for a single task, ensuring high cohesion and low coupling.
- **Open/Closed Principle (OCP):** The system is designed to be open for extension but closed for modification, allowing for easy addition of new functionality.
- **Liskov Substitution Principle (LSP):** Derived classes (e.g., simulation handlers) can be substituted for their base classes (e.g., simulation interface) without affecting the functionality of the system.
- **Interface Segregation Principle (ISP):** Interfaces are segregated based on client requirements, preventing clients from depending on methods they don't use.
- **Dependency Inversion Principle (DIP):** High-level modules (e.g., simulation handlers) depend on abstractions (e.g., interfaces), rather than concrete implementations, promoting flexibility and testability.

#### 5. Project Workflow
1. **Input Collection:** Users input field size, initial car positions, orientations, and commands.
2. **Simulation Initialization:** Based on the simulation type, appropriate handlers are initialized.
3. **Simulation Execution:** The simulation handler processes commands for each car and detects collisions.
4. **Output Display:** Simulation results, including final car positions, orientations, and collisions, are displayed.

#### 6. Other Relevant Details
- **Error Handling:** Includes mechanisms for input validation and format exceptions.
- **Extensibility:** The modular design allows for easy extension with additional simulation types, input sources, or output formats.

### Unit/Integration Test - CarSimulation.UnitTests

#### Code Structure

```markdown
└── CarSimulation.UnitTests
    │   CarSimulation.UnitTests.csproj
    │   
    └── Tests
        │   CommandBehaviorTests.cs
        │   MultipleCarsSimulationTests.cs
        │   SingleCarSimulationTests.cs
        │       
        └── UtilityHandlers
                TestMultipleCarsInputHandler.cs
                TestOutputHandler.cs
                TestSingleCarInputHandler.cs

```

#### 1. Test Details
The CarSimulation.UnitTests project contains test classes responsible for testing various components and functionalities of the simulation system. These tests are designed to ensure that the implemented logic behaves as expected under different scenarios and inputs.

#### 2. Test Classes

##### CommandBehaviorTests
- This class contains tests for the behavior of individual commands used in the simulation system, including MoveForwardCommand, TurnLeftCommand, and TurnRightCommand. Each test method verifies that the corresponding command executes correctly and produces the expected results when applied to a car object.

##### MultipleCarsSimulationTests
- This class contains tests for the behavior of the multiple-cars simulation handler. It includes tests for scenarios where multiple cars are simulated simultaneously, ensuring that collisions are detected and reported accurately. The tests cover cases with no collisions, collisions between two cars, and collisions between more than two cars in both the same and different positions.

##### SingleCarSimulationTests
- This class contains tests for the behavior of the single-car simulation handler. It includes tests for simulating a single car's movement and verifying its final position and orientation after executing a sequence of commands. Additionally, it tests scenarios where the car ignores commands that would cause it to move out of bounds.

#### 3. Utility Handlers
The `UtilityHandlers` namespace contains utility classes used specifically for testing purposes, such as input handlers and output handlers.

- **TestMultipleCarsInputHandler:** This class implements the IInputHandler interface and provides a custom input configuration for testing multiple-cars simulations. It allows specifying predefined car inputs and commands for testing different simulation scenarios.

- **TestOutputHandler:** This class implements the IOutputHandler interface and serves as a test output handler. It captures the output generated during simulations for assertion in test methods, allowing verification of expected output.

- **TestSingleCarInputHandler:** This class implements the IInputHandler interface and provides a custom input configuration for testing single-car simulations. Similar to TestMultipleCarsInputHandler, it allows specifying predefined car inputs and commands tailored for single-car simulation testing scenarios.

#### 4. Design Principles Followed
The unit tests adhere to the following design principles:
- **Test Coverage:** Each test class aims to cover specific components and functionalities of the system to ensure comprehensive testing.
- **Isolation:** Tests are designed to be independent of each other to isolate failures and facilitate debugging.
- **Arrange-Act-Assert (AAA) Pattern:** Tests follow the AAA pattern, where setup (Arrange), execution (Act), and verification (Assert) are clearly separated for readability and maintainability.
- **Test Data Management:** Test classes use predefined test data and inputs to ensure consistent and reproducible test results.

```markdown
Known limitations
-- Handle only the first colision:** Program handles only the first  collision if there are more than 2 cars and there are more than 1 collision happens in single run.

Assumptions
-- Program  assume that all cars in multi car simulation start in same time and process the same command step number at any given time.
```

![Screenshot 2024-04-08 234118](https://github.com/amasen02/AutoDrivingCarSimulation/assets/97525823/5c635663-2c70-457e-914d-6424feb10058)

![Screenshot 2024-04-08 174016](https://github.com/amasen02/AutoDrivingCarSimulation/assets/97525823/e5471bcd-51bf-40a2-bbb7-0007a0b016cd)



