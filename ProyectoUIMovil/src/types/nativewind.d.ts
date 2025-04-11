import 'nativewind/types';
import '@react-navigation/native';
declare module 'react-native' {
  interface ViewProps {
    className?: string;
  }
  interface TextProps {
    className?: string;
  }
  interface ScrollViewProps {
    className?: string;
  }
  // Extiende otros componentes según necesites
  declare module '@react-navigation/native' {
    export interface NavigatorProps {
      id?: string | undefined; 
    }
  }
}
