import '@react-navigation/native';

// Extensión de tipos para React Navigation
declare module '@react-navigation/native' {
  export interface NavigatorProps {
    id?: string | undefined; // Solución híbrida
  }
}

// Tipos globales para tu aplicación
declare global {
  namespace ReactNavigation {
    interface RootParamList {
      Home: undefined;
      Complaints: undefined;
      Profile: { userId?: string };
    }
  }
}