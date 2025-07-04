# TweenAction

**Author: Minh Mino**

`TweenAction` is inspired by **[DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)** and **[LeanTween](https://assetstore.unity.com/packages/tools/animation/leantween-3595)**.

Creating my own tween system helped me learn how tweening works under the hood, while also allowing me to add or remove features tailored to my specific needs during development.

---

## 🌀 Tween

`Tween` is the core handler of the system. To use it, simply call:

```csharp
Tween.Target(gameObject)
```

This will create (or retrieve) a `Tween` component on the `GameObject`, and you can assign a chain of actions (`TweenOrder`) to it.

### 📌 Basic Example

```csharp
Tween.Target(gameObject)
     .BreakAndAppend(1, (progress) => {
         _transform.position = _positionA + (_positionB - _positionA) * progress;
     }, _leanEase)
     .BreakAndAppend(1, (progress) => {
         _transform.position = _positionB + (_positionA - _positionB) * progress;
     }, _leanEase)
     .Repeat(-1)
     .StartAll();
```

---

## 🔧 TweenOrder

`TweenOrder` defines a single tweening action. The `onUpdate` function is looped during its runtime, returning a `progress` value from `0` to `1`, based on your custom easing logic.

You are responsible for defining what the action should do.

### 📌 Example

```csharp
TweenOrder tweenOrder = new TweenOrder(
    1,
    (progress) => {
        _transform.localScale = Vector3.one + (new Vector3(5, 5, 5) - Vector3.one) * progress;
    },
    _leanEase
);

Tween.Target(gameObject)
     .Append(tweenOrder)
     .StartAll();
```

---

## ➕ Append

All `TweenOrder`s appended sequentially will **run concurrently**, regardless of their individual durations. They are grouped into a **single list**, and the total duration of the list equals the longest `TweenOrder` within that group.

### 📌 Example

```csharp
TweenOrder tweenOrderScale = new TweenOrder(
    1,
    (progress) => {
        _transform.localScale = Vector3.one + (new Vector3(5, 5, 5) - Vector3.one) * progress;
    },
    _leanEase
);

TweenOrder tweenOrderMove = new TweenOrder(
    1,
    (progress) => {
        _transform.position = _positionA + (_positionB - _positionA) * progress;
    },
    _leanEase
);

Tween.Target(gameObject)
     .Append(tweenOrderScale)
     .Append(tweenOrderMove)
     .StartAll();
```

👉 In this example, `tweenOrderScale` and `tweenOrderMove` will execute **simultaneously**.

---

## 🔀 BreakAndAppend

`BreakAndAppend` creates a **new list** and appends the action into it. This list will **only start after the previous list has completed** all of its actions.

### 📌 Example

```csharp
Tween.Target(gameObject)
     .BreakAndAppend(1, (progress) => {
         _transform.position = _positionA + (_positionB - _positionA) * progress;
     }, _leanEase)
     .BreakAndAppend(1, (progress) => {
         _transform.position = _positionB + (_positionA - _positionB) * progress;
     }, _leanEase)
     .Repeat(-1)
     .StartAll();
```

---

## ▶️ StartAll

You **must** call `.StartAll()` to activate the tween. If not called, the tween will **not run**.

---

## ⏸ Pause & Resume

These methods are useful when you want to **temporarily pause or resume** tweens on a specific target.

---

## ⛔ StopAll

Calling `StopAll()` will **immediately stop** all tweens on the target and **apply their final state**.

---

## ⚙️ Events: OnStart & OnComplete

Each `TweenOrder` can define `OnStart()` and `OnComplete()` callbacks.

### 📌 Example

```csharp
TweenOrder tweenOrderScale = new TweenOrder(
    1,
    (progress) => {
        _transform.localScale = Vector3.one + (new Vector3(5, 5, 5) - Vector3.one) * progress;
    },
    _leanEase
).OnStart(() => Debug.Log("Start Scale"))
 .OnComplete(() => Debug.Log("Complete Scale"));

TweenOrder tweenOrderMove = new TweenOrder(
    1,
    (progress) => {
        _transform.position = _positionA + (_positionB - _positionA) * progress;
    },
    _leanEase
).OnStart(() => Debug.Log("Start Move"))
 .OnComplete(() => Debug.Log("Complete Move"));

Tween.Target(gameObject)
     .Append(tweenOrderScale)
     .Append(tweenOrderMove)
     .StartAll();
```

---

## 💬 Feedback & Contributions

If you find this library useful or have ideas for improvements, feel free to open an issue or submit a pull request!

---

**Minh Mino**
