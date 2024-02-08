import './App.scss';
import { Route, Routes } from 'react-router-dom';
import Main from './views/Main/Main';
import About from './views/About/About';
import Contact from './views/Contact/Contact';
import Locations from './views/Locations/Locations';
import Footer from './components/Footer/Footer';
import Wines from './views/Wines/Wines';
import Wineries from './views/Wineries/Wineries';
import WineryDetailsPage from './views/WineryDetails/WineryDetails';
import Privacy from './views/Privacy/Privacy';
import TermsAndConditions from './views/TermsAndConditions/TermsAndConditions';
import FrequentlyAskedQuestions from './views/FrequentlyAskedQuestions/FrequentlyAskedQuestions';
import WineDetailsPage from './views/WineDetails/WineDetails';

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={ <Main /> } />
        <Route path="/wineries" element={ <Wineries /> } />
        <Route path="/locations" element={ <Locations /> } />
        <Route path="/contact" element={ <Contact /> } />
        <Route path="/aboutus" element={ <About /> } />
        <Route path="/wines" element={ <Wines /> } />
        <Route path="/winery" element={ <WineryDetailsPage /> } />
        <Route path="/wine" element={ <WineDetailsPage /> } />
        <Route path="/privacy" element={ <Privacy /> } />
        <Route path="/termsandconditions" element={ <TermsAndConditions /> } />
        <Route path="/faq" element={ <FrequentlyAskedQuestions /> } />
      </Routes>
      <Footer />
    </div>
  );
}

export default App;
