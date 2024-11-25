import './App.css';
import { AuthProvider } from 'oidc-react';
import LoggedIn from './LoggedIn';

const oidcConfig = {
  authority: 'http://localhost:8080/realms/test/',
  clientId: 'securewebapplication',
  responseType: 'code',
  redirectUri: 'http://localhost:3000/',
  onSignIn: async (user: any) => {
    console.log(user);
    window.location.hash = '';
  }
};

function App() {
  return (
    <AuthProvider {...oidcConfig}>
      <div className="App">
        <header className="App-header">
          <LoggedIn />
        </header>
      </div>
    </AuthProvider>
  );
}

export default App;
