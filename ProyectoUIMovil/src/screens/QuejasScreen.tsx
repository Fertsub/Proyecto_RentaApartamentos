import React, { useState } from "react";
import { View, Text, Pressable, TextInput, ScrollView, ActivityIndicator } from "react-native";
import { Picker } from "@react-native-picker/picker";
import { Card, Button } from "react-native-paper";
import axios, { AxiosError } from "axios";
import { ArrowLeft, Send } from "lucide-react-native";
import { NativeStackScreenProps } from "@react-navigation/native-stack";
import { RootStackParamList } from "../navigation/AppNav";

// Tipos para el formulario
type ComplaintCategory = "noise" | "security" | "cleanliness" | "neighbors" | "other";

interface ComplaintForm {
  subject: string;
  category: ComplaintCategory;
  description: string;
  photo: string | null;
}

type Props = NativeStackScreenProps<RootStackParamList, "Complaints">;

export default function ComplaintsPage({ navigation }: Props) {
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [formData, setFormData] = useState<ComplaintForm>({
    subject: "",
    category: "noise",
    description: "",
    photo: null,
  });

  const handleSubmit = async (): Promise<void> => {
    setIsSubmitting(true);
    try {
      const response = await axios.post<{ success: boolean }>(
        "https://tu-api.com/complaints",
        formData
      );
      
      if (response.data.success) {
        alert("¡Queja enviada con éxito!");
        // Opcional: resetear el formulario después del envío
        setFormData({
          subject: "",
          category: "noise",
          description: "",
          photo: null,
        });
        // Opcional: navegar a otra pantalla
        // navigation.navigate("Home");
      }
    } catch (error) {
      const axiosError = error as AxiosError;
      alert(axiosError.message || "Error al enviar la queja");
      console.error(error);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <ScrollView className="flex-1 bg-gray-50 p-4">
      <View className="flex-row items-center mb-6">
        <Pressable onPress={() => navigation.goBack()}>
          <ArrowLeft size={24} color="#000" />
        </Pressable>
        <Text className="text-xl font-bold ml-4">Quejas</Text>
      </View>

      <Card>
        <Card.Title title="Enviar una queja" subtitle="Complete el formulario para enviar una queja" />
        <Card.Content>
          <View className="mb-4">
            <Text className="mb-2 font-medium">Asunto</Text>
            <TextInput
              placeholder="Asunto de la queja"
              value={formData.subject}
              onChangeText={(text) => setFormData({ ...formData, subject: text })}
              className="border p-2 rounded"
            />
          </View>

          <View className="mb-4">
            <Text className="mb-2 font-medium">Categoría</Text>
            <Picker
              selectedValue={formData.category}
              onValueChange={(value: ComplaintCategory) => 
                setFormData({ ...formData, category: value })
              }
              className="border rounded"
            >
              <Picker.Item label="Ruido" value="noise" />
              <Picker.Item label="Seguridad" value="security" />
              <Picker.Item label="Limpieza" value="cleanliness" />
              <Picker.Item label="Vecinos" value="neighbors" />
              <Picker.Item label="Otro" value="other" />
            </Picker>
          </View>

          <View className="mb-4">
            <Text className="mb-2 font-medium">Descripción</Text>
            <TextInput
              placeholder="Describa su queja en detalle"
              multiline
              numberOfLines={5}
              value={formData.description}
              onChangeText={(text) => setFormData({ ...formData, description: text })}
              className="border p-2 rounded h-32"
            />
          </View>
        </Card.Content>
        <Card.Actions>
          <Button
            mode="contained"
            onPress={handleSubmit}
            disabled={isSubmitting}
            className="w-full bg-blue-500"
          >
            {isSubmitting ? (
              <ActivityIndicator color="#fff" />
            ) : (
              <Text className="text-white flex-row items-center">
                <Send size={16} color="#fff" className="mr-2" />
                Enviar queja
              </Text>
            )}
          </Button>
        </Card.Actions>
      </Card>
    </ScrollView>
  );
}