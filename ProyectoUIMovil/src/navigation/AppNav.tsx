// src/navigation/AppNav.tsx
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { NavigationContainer } from "@react-navigation/native";
import React from "react";

// Importa tus pantallas (ajusta las rutas según tu estructura)
import HomeScreen from "../screens/Homescreen";
import ProfileScreen from "../screens/ProfileScreen";
import QuejasScreen from "../screens/QuejasScreen";

// Define los tipos de rutas
export type RootStackParamList = {
  Home: undefined;
  Complaints: undefined;
  Profile: { userId?: string }; // Ejemplo de parámetro
};

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function AppNavigator() {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Home" id="root">
        <Stack.Screen 
          name="Home" 
          component={HomeScreen} 
          options={{ title: "Inicio" }}
        />
        <Stack.Screen 
          name="Complaints" 
          component={QuejasScreen} 
          options={{ title: "Enviar Queja" }}
        />
        <Stack.Screen 
          name="Profile" 
          component={ProfileScreen} 
          options={{ title: "Perfil" }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
}