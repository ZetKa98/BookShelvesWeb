import logo from './logo.svg';
import './App.css';
import {BookShelves} from './BookShelves'
import {BrowserRouter} from 'react-router-dom'


function App() {
  return (
    <BrowserRouter>
      <div className="m-3 d-flex justify-content-center">
        <h3>Book shelves</h3>
      </div>

      <BookShelves></BookShelves>

    </BrowserRouter>
  );
}

export default App;