import { registerRootComponent } from 'expo';
// Import the global.css file in the index.js file:
import App from './src/App';
import './src/global.css';

// registerRootComponent calls AppRegistry.registerComponent('main', () => App);
// It also ensures that whether you load the app in Expo Go or in a native build,
// the environment is set up appropriately
registerRootComponent(App);
