import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Main from './routes/Main';
import Notice from './routes/Notice';
import ItemInfo from './routes/ItemInfo';
import Comunity from './routes/Comunity';
import Rank from './routes/Rank';
import SignUp from './routes/SignUp';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Main />}></Route>
        <Route path='/notice' element={<Notice />}></Route>
        <Route path='/itemInfo' element={<ItemInfo />}></Route>
        <Route path='/comunity' element={<Comunity />}></Route>
        <Route path='/rank' element={<Rank />}></Route>
        <Route path='/signup' element={<SignUp />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
