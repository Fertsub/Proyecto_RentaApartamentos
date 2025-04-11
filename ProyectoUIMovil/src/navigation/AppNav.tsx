import React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

// Importa tus pantallas
import HomeScreen from "../screens/Homescreen";
import ProfileScreen from "../screens/ProfileScreen";
import QuejasScreen from "../screens/QuejasScreen";

// Define los tipos de rutas
export type RootStackParamList = {
  Home: undefined;
  Complaints: undefined;
  Profile: { userId?: string }; // ejemplo con parámetro opcional
};

const Stack = createNativeStackNavigator<RootStackParamList>();

export default function AppNavigator() {
  return (
    <NavigationContainer>
      <Stack.Navigator
        initialRouteName="Home"
        id={undefined}
        screenOptions={{
          headerStyle: { backgroundColor: "#f9fafb" },
          headerTitleStyle: { fontFamily: "Poppins-Bold" },
          contentStyle: { backgroundColor: "#ffffff" },
        }}
      >
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
