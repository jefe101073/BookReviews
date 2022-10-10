import { StyleSheet } from 'react-native';
import { loadAuthors } from '../api';

import EditScreenInfo from '../components/EditScreenInfo';
import { Text, View } from '../components/Themed';
import { RootTabScreenProps } from '../types';

export default function AuthorsScreen({ navigation }: RootTabScreenProps<'AuthorsTab'>) {

  const authors = loadAuthors();
  console.log(authors);

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Authors</Text>
      <View style={styles.separator} lightColor="#eee" darkColor="rgba(255,255,255,0.1)" />
      <EditScreenInfo path="/screens/AuthorsScreen.tsx" />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  title: {
    fontSize: 20,
    fontWeight: 'bold',
  },
  separator: {
    marginVertical: 30,
    height: 1,
    width: '80%',
  },
});
