import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Main from './routes/Main';
import Story from './routes/Story';
import ItemInfo from './routes/ItemInfo';
import Comunity from './routes/Comunity';
import Rank from './routes/Rank';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Main />}></Route>
        <Route path='/story' element={<Story />}></Route>
        <Route path='/itemInfo' element={<ItemInfo />}></Route>
        <Route path='/comunity' element={<Comunity />}></Route>
        <Route path='/rank' element={<Rank />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
