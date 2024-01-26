import styled from "styled-components"
import { colors } from "../enums"
import { InputHTMLAttributes, useCallback } from "react"
import React from "react"


const Container = styled.div<Props>`
  width: ${(props) => props.width ? `${props.width}px` : '457px'};
  display: flex;
  flex-direction: column;
`

const Title = styled.p`
  font-weight: 700;
  font-size: 22px;
  line-height: 28px;
  letter-spacing: 0.8px;
`

const Input = styled.input<Props>`
    border: none;
    color: ${(props) => props.isError ? colors.red : 'none'};
    border-bottom: 0.5px ${colors.dirtGray} solid;
    padding: 19px 0;
    width: ${(props) => props.width ? `${props.width}px` : '457px'};
    word-break: break-word;
    text-align: ${(props) => props.textAlign};
    font-size: 16px;
    &:focus {
      outline: none;
    }
`

interface Props extends InputHTMLAttributes<HTMLInputElement> {
    width?: string;
    title?: string;
    placeholder?: string;
    validate?: 'text' | 'password' | 'number'
    types?: 'text' | 'photo';
    value?: string;
    isRequired?: boolean;
    textAlign?: 'center' | 'left';
    onChangeValue?: (value : string) => void;
    isError?: boolean;
    onClick?: () => void;
    min?: number;
}

const InputComponent : React.FC<Props> = ({
  width,
  title,
  placeholder,
  value,
  validate,
  isRequired,
  textAlign,
  onChangeValue,
  isError,
  min,
}) => {

  const onChangeHandler = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
    if (onChangeValue) {
      return onChangeValue(e.target.value)
    }
  }, [onChangeValue])

  return (
    <Container
      width={width}
    >
      {title && (
        <Title>{title}</Title>
        )
      }
      <Input
        min={min}
        width={width}
        placeholder={placeholder}
        value={value}
        type={validate}
        required={isRequired}
        textAlign={textAlign}
        onChange={onChangeHandler}
        isError={isError}
      />
    </Container>
  )
}

export default InputComponent;