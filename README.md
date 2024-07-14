
# Traffic Light Simulation

This repository contains a simulation of a traffic light system using the State Machine pattern and Factory Method in C#. The purpose of this project is educational, demonstrating a robust approach to traffic light management. Using Unity 6.

## Features
- **State Machine Pattern**: Manages traffic light states (Red, Red/Amber, Amber, Green).
- **Factory Method**: Creates traffic data container dynamically.
- **Configurable Durations**: Allows customization of signal durations via scriptable objects.

## Installation
1. Clone the repository.
   ```sh
   git clone https://github.com/berkterek/traffic_light_repo.git
   ```
2. Open the project in Unity.

## Usage
1. Customize the signal durations using the provided scriptable objects.
2. Run the simulation and observe the traffic light state changes.
3. Interact with the UI to see the current signal state and check if driving is allowed.

## Code Structure
- `TrafficLightController`: Main class managing the traffic light logic.
- `GeneralStateMachine`: Handles state transitions.
- `TrafficLightDataResourceFactory`: Creates instances or get instance of traffic data container.
- `RedState`, `RedAmberState`, `AmberState`, `GreenState`: Represent different states of the traffic light.

## Contributing
1. Fork the repository.
2. Create a new branch.
3. Make your changes.
4. Submit a pull request.

## License
This project is licensed under the Unlicense License.

For more details, refer to the repository [here](https://github.com/berkterek/traffic_light_repo).
