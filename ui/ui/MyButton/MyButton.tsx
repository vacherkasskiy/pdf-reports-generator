import React from "react";
import styles from './MyButton.module.scss'
import {getSizeStyle, getThemeStyle, getTypeStyle, size, theme, type} from "../utils";

type typeProperty = 'button' | 'submit' | 'reset'

interface MyButtonProps {
    text: string
    disabled?: boolean
    typeProperty?: typeProperty
    size?: size
    type?: type
    theme: theme
    onClick?: (() => void)
}

function MyButton({text, disabled, onClick, size, type, theme, typeProperty}: MyButtonProps): React.ReactNode {
    const disabledBool: boolean = disabled ?? false;

    const getDisabledStyle = (): string => {
        if (disabledBool) return styles.Disabled;

        return '';
    }

    const buttonStyle =
        styles.my_button + ' ' +
        getDisabledStyle() + ' ' +
        getSizeStyle(size) + ' ' +
        getTypeStyle(type) + ' ' +
        getThemeStyle(theme)

    return (
        <button
            disabled={disabledBool}
            className={buttonStyle}
            onClick={onClick}
            type={typeProperty || 'button'}
        >
            {text}
        </button>
    )
}

export default MyButton