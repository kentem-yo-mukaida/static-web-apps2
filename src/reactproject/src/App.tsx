import React, { useState, useEffect, useRef } from 'react'
import logo from './logo.svg';
import './App.css';

const App = () => {
    const [posts, setPosts] = useState('');
    const value1Ref = useRef(null);
    const value2Ref = useRef(null);

    const handlerClick = () => {
        const value1Element = value1Ref.current as HTMLInputElement | null;
        const value2Element = value1Ref.current as HTMLInputElement | null;
        if (!value1Element || !value2Element) {
            return;
        }
        const value1 = Number(value1Element.value);
        const value2 = Number(value2Element.value);
        if (!Number.isInteger(value1) || !Number.isInteger(value2)) {
            alert('“ü—Í’l‚ª•s³');
            return;
        }

        fetch(`/api/sum?value1=${value1}&value2=${value2}`, { method: 'GET' })
            .then(q => q.json())
            .then(q => {
                setPosts(q.value);
                alert(q.value);
            })
            .catch(q => {
                console.log(q);
            });
    };

    return (
        <div className="App">
            Hello World!
            <input ref={value1Ref} ></input> +
            <input ref={value2Ref} ></input>
            <input type={'button'} value="SUM" onClick={handlerClick} ></input> =
            <label>{posts}</label>
        </div>
    );
}

export default App;
