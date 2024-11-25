import { useAuth } from 'oidc-react';

const LoggedIn = () => {
  const auth = useAuth();
  if (auth && auth.userData) {
    return (
      <div>
        <strong>Welcome {auth.userData.profile.name}! 🎉</strong><br />
        <button onClick={() => auth.signOutRedirect()}>Log out!</button>
      </div>
    );
  }
  return (
    <div>
      <strong>Not logged in!</strong><br />
      <button onClick={() => auth.signIn()}>Sign in!</button>
    </div>
  );
};

export default LoggedIn;